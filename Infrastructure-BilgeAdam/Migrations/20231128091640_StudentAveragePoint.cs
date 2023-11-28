using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_BilgeAdam.Migrations
{
    /// <inheritdoc />
    public partial class StudentAveragePoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ProjectExam",
                table: "Students",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectExam",
                table: "Students");
        }
    }
}
