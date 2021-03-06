﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Framework.Configuration;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity.Metadata.Builders;

namespace AllReady.Models
{
  public class AllReadyContext : IdentityDbContext<ApplicationUser>
  {
    private IConfiguration _configuration;
    private IHostingEnvironment _environment;

    public AllReadyContext(IConfiguration configuration, IHostingEnvironment environment)
    {
      _configuration = configuration;
      _environment = environment;

      // Create DB and do migrations if necessary
      Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (_environment.IsDevelopment())
      {
        if (_configuration["Data:DefaultConnection:UseInMemory"].ToLowerInvariant() == "true")
        {
          optionsBuilder.UseInMemoryDatabase();
        }
        else
        {
          optionsBuilder.UseSqlServer(_configuration["Data:DefaultConnection:LocalConnectionString"]);
        }
      }
      else
      {
        optionsBuilder.UseSqlServer(_configuration["Data:DefaultConnection:AzureConnectionString"]);

      }
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<ActivitySignup> ActivitySignup { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<PostalCodeGeo> PostalCodes { get; set; }
    public DbSet<AllReadyTask> Tasks { get; set; }
    public DbSet<TaskUsers> TaskSignup { get; set; }
    public DbSet<Resource> Resources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.UseSqlServerIdentityColumns();

      Map(modelBuilder.Entity<Campaign>());
      Map(modelBuilder.Entity<CampaignSponsors>());
      Map(modelBuilder.Entity<Activity>());
      Map(modelBuilder.Entity<ActivitySignup>());
      Map(modelBuilder.Entity<AllReadyTask>());
      Map(modelBuilder.Entity<TaskUsers>());
      Map(modelBuilder.Entity<Location>());
      Map(modelBuilder.Entity<PostalCodeGeo>());

    }

    private void Map(EntityTypeBuilder<PostalCodeGeo> builder)
    {
      builder.HasKey(k => k.PostalCode);
    }

    private void Map(EntityTypeBuilder<Location> builder)
    {
      builder.HasOne(l => l.PostalCode);
    }

    private void Map(EntityTypeBuilder<TaskUsers> builder)
    {
      builder.HasOne(u => u.Task);
    }

    private void Map(EntityTypeBuilder<AllReadyTask> builder)
    {
      builder.HasOne(t => t.Activity);
      builder.HasOne(t => t.Tenant);
      builder.HasMany(t => t.AssignedVolunteers);
    }

    private void Map(EntityTypeBuilder<ActivitySignup> builder)
    {
      builder.HasOne(t => t.User);
      builder.HasOne(t => t.Activity);
      builder.Property(t => t.SignupDateTime)
             .IsRequired();
    }

    private void Map(EntityTypeBuilder<Activity> builder)
    {
      builder.HasOne(a => a.Tenant);
      builder.HasOne(a => a.Campaign);
      builder.HasOne(a => a.Location);
      builder.HasMany(a => a.Tasks);
      builder.HasMany(a => a.UsersSignedUp);
    }

    private void Map(EntityTypeBuilder<CampaignSponsors> builder)
    {
      builder.HasOne(s => s.Campaign)
             .WithMany(c => c.ParticipatingTenants);
      builder.HasOne(s => s.Tenant);

    }

    private void Map(EntityTypeBuilder<Campaign> builder)
    {
      builder.HasOne(c => c.ManagingTenant);
      builder.HasMany(c => c.Activities);
      builder.Property(a => a.Name).IsRequired();
    }
  }
}
