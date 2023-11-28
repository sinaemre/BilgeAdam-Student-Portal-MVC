using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure_BilgeAdam.Context.IdentityContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2941c96f-580d-4fa1-a18f-80dc158b28cb", null, "admin", "ADMIN" },
                    { "51244080-0979-4716-a6b4-d9eca69706ab", null, "teacher", "TEACHER" },
                    { "545c4c20-cb0d-4525-a2ff-f9abfe520851", null, "ikPersonel", "IKPERSONEL" },
                    { "732afff8-a944-45bd-aa94-770cd95f060e", null, "student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { "b83692b7-aaa5-41c9-a97b-5123da9f6eb0", 0, "16ddcaf4-4375-4400-9cfb-e4854b90a9c9", new DateTime(2023, 11, 28, 16, 10, 2, 194, DateTimeKind.Local).AddTicks(6912), null, "ikpersonel@test.com", false, true, null, "IKPERSONEL@TEST.COM", "IKPERSONEL", "AQAAAAIAAYagAAAAEL+CnLrEusC6h46UsIdEfVplT+X0W3PSfz61gk7ehlQu34KzfzswUgTHYZBSRK2nGQ==", null, false, "4742e038-5d79-4615-b6b2-01559c84194e", 1, false, null, "ikPersonel" },
                    { "c0333990-76bc-41c7-9b36-f4b99b6aa33d", 0, "f8e15377-3beb-400a-8502-dda1c54f0322", new DateTime(2023, 11, 28, 16, 10, 1, 938, DateTimeKind.Local).AddTicks(6800), null, "student2@test.com", false, true, null, "STUDENT2@TEST.COM", "STUDENT2", "AQAAAAIAAYagAAAAEFaD2IBNWapZUnOQDtiMVLv+uIg/oKqA8F0wYez3CIkk7RSvONqFMT9Cek/4379bGA==", null, false, "a451c1de-dd43-48d0-a330-8f217ce572f8", 1, false, null, "student2" },
                    { "cea12c00-6fae-4243-ae02-e9498d11a3e5", 0, "3891a373-9bff-4535-b8cd-5651138bb9d9", new DateTime(2023, 11, 28, 16, 10, 1, 811, DateTimeKind.Local).AddTicks(1841), null, "student@test.com", false, true, null, "STUDENT@TEST.COM", "STUDENT", "AQAAAAIAAYagAAAAEJPISX7OAocfoZXGg6MtfAZPeGov/2ph68pEfEtnScVzMdAVAHjhMbZ5/GYNuNjkGg==", null, false, "12dc9e57-3945-4e36-92e7-000773e4f669", 1, false, null, "student" },
                    { "d7e6ce89-4fdb-4dab-b53f-0d55874de67b", 0, "b0ff9866-a096-48aa-bdc8-1bdff8352209", new DateTime(2023, 11, 28, 16, 10, 2, 73, DateTimeKind.Local).AddTicks(6066), null, "teacher@test.com", false, true, null, "TEACHER@TEST.COM", "TEACHER", "AQAAAAIAAYagAAAAEG1Qywvj3Ofuow1u+ZFRnDvgp52vGZmhWUBr8OiCDoUmm7HIUVz2ZIiDhs+FdPLQXg==", null, false, "9e1baf6c-0503-4f7c-82f4-dcc29c0783e5", 1, false, null, "teacher" },
                    { "e3569982-6bcc-49ed-b599-6b67ae72d134", 0, "f7a6a045-9bc9-4720-b3de-0bb018f802e3", new DateTime(2023, 11, 28, 16, 10, 1, 677, DateTimeKind.Local).AddTicks(8279), null, "admin@test.com", false, true, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAIAAYagAAAAEDEHMuUAzB+P5Nkd/cmYbWKM3uyIVMwD8v6mFvp6Wop+f/BDcy6MHq6a1u/A4KWr8w==", null, false, "0a102cb5-a024-4e9f-91a0-96b918f366eb", 1, false, null, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "545c4c20-cb0d-4525-a2ff-f9abfe520851", "b83692b7-aaa5-41c9-a97b-5123da9f6eb0" },
                    { "732afff8-a944-45bd-aa94-770cd95f060e", "c0333990-76bc-41c7-9b36-f4b99b6aa33d" },
                    { "732afff8-a944-45bd-aa94-770cd95f060e", "cea12c00-6fae-4243-ae02-e9498d11a3e5" },
                    { "51244080-0979-4716-a6b4-d9eca69706ab", "d7e6ce89-4fdb-4dab-b53f-0d55874de67b" },
                    { "2941c96f-580d-4fa1-a18f-80dc158b28cb", "e3569982-6bcc-49ed-b599-6b67ae72d134" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
