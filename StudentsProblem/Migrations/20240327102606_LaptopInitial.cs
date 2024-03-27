using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsProblem.Migrations
{
    /// <inheritdoc />
    public partial class LaptopInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Address_AddressId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Address_AddressId",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorCourse_Id",
                table: "ProfessorCourse",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_School_Address_AddressId",
                table: "School",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Address_AddressId",
                table: "Students",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Address_AddressId",
                table: "School");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Address_AddressId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorCourse_Id",
                table: "ProfessorCourse");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Address_AddressId",
                table: "School",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Address_AddressId",
                table: "Students",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
