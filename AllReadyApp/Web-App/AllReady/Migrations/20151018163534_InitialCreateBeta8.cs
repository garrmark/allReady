using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace AllReady.Migrations
{
    public partial class InitialCreateBeta8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostalCodeGeo",
                columns: table => new
                {
                    PostalCode = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeGeo", x => x.PostalCode);
                });
            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryTag = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MediaUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PublishDateBegin = table.Column<DateTime>(nullable: false),
                    PublishDateEnd = table.Column<DateTime>(nullable: false),
                    ResourceUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogoUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    WebUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PostalCodePostalCode = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_PostalCodeGeo_PostalCodePostalCode",
                        column: x => x.PostalCodePostalCode,
                        principalTable: "PostalCodeGeo",
                        principalColumn: "PostalCode");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AssociatedTenantId = table.Column<int>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_Tenant_AssociatedTenantId",
                        column: x => x.AssociatedTenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    EndDateTimeUtc = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ManagingTenantId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OrganizerId = table.Column<string>(nullable: true),
                    StartDateTimeUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaign_Tenant_ManagingTenantId",
                        column: x => x.ManagingTenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Campaign_ApplicationUser_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EndDateTimeUtc = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OrganizerId = table.Column<string>(nullable: true),
                    StartDateTimeUtc = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activity_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activity_ApplicationUser_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activity_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "CampaignSponsors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignId = table.Column<int>(nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignSponsors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignSponsors_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CampaignSponsors_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "ActivitySignup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityId = table.Column<int>(nullable: true),
                    CheckinDateTime = table.Column<DateTime>(nullable: true),
                    SignupDateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySignup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivitySignup_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActivitySignup_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "AllReadyTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EndDateTimeUtc = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StartDateTimeUtc = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllReadyTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllReadyTask_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AllReadyTask_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "TaskUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(nullable: true),
                    StatusDateTimeUtc = table.Column<DateTime>(nullable: false),
                    StatusDescription = table.Column<string>(nullable: true),
                    TaskId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskUsers_AllReadyTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "AllReadyTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskUsers_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ActivitySignup");
            migrationBuilder.DropTable("CampaignSponsors");
            migrationBuilder.DropTable("Resource");
            migrationBuilder.DropTable("TaskUsers");
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("AllReadyTask");
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("Activity");
            migrationBuilder.DropTable("Campaign");
            migrationBuilder.DropTable("Location");
            migrationBuilder.DropTable("AspNetUsers");
            migrationBuilder.DropTable("PostalCodeGeo");
            migrationBuilder.DropTable("Tenant");
        }
    }
}
