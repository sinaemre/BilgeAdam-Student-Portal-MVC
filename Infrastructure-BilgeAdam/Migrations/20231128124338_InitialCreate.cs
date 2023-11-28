using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure_BilgeAdam.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassroomNo = table.Column<byte>(type: "smallint", nullable: false),
                    ClassroomName = table.Column<string>(type: "text", nullable: true),
                    ClassroomDescription = table.Column<string>(type: "text", nullable: true),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classrooms_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Exam1 = table.Column<double>(type: "double precision", nullable: true),
                    Exam2 = table.Column<double>(type: "double precision", nullable: true),
                    ProjectExam = table.Column<double>(type: "double precision", nullable: true),
                    ProjectPath = table.Column<string>(type: "text", nullable: true),
                    ClassroomId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "Status", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2023, 11, 28, 15, 43, 38, 12, DateTimeKind.Local).AddTicks(7362), null, "teacher@test.com", "Sina Emre", "Teacher", 1, null });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "Id", "ClassroomDescription", "ClassroomName", "ClassroomNo", "CreatedDate", "DeletedDate", "Status", "TeacherId", "UpdatedDate" },
                values: new object[] { 1, "320 Saat .NET Full Stack Developer Eğitimi", "YZL-8147", (byte)10, new DateTime(2023, 11, 28, 15, 43, 38, 12, DateTimeKind.Local).AddTicks(8094), null, 1, 1, null });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "ClassroomId", "CreatedDate", "DeletedDate", "Email", "Exam1", "Exam2", "FirstName", "LastName", "ProjectExam", "ProjectPath", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 11, 28, 15, 43, 38, 12, DateTimeKind.Local).AddTicks(8567), null, "student@test.com", null, null, "Sina Emre", "Öğrenci", null, null, 1, null },
                    { 2, new DateTime(1999, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 11, 28, 15, 43, 38, 12, DateTimeKind.Local).AddTicks(8590), null, "student2@test.com", null, null, "Test", "Öğrenci", null, null, 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_TeacherId",
                table: "Classrooms",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassroomId",
                table: "Students",
                column: "ClassroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
