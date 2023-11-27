using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_BilgeAdam.Migrations
{
    /// <inheritdoc />
    public partial class StudentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Exam1",
                table: "Students",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Exam2",
                table: "Students",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectPath",
                table: "Students",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exam1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Exam2",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProjectPath",
                table: "Students");
        }
    }
}
