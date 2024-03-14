using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsProblem.Migrations
{
    /// <inheritdoc />
    public partial class ProfessorIdIsNotNullableInProfessorCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "ProfessorCourse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProfessorId",
                table: "ProfessorCourse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
