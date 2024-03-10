using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsProblem.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInSchoolAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Address_AddressId",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_School_AddressId",
                table: "School");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "School",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_School_AddressId",
                table: "School",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_School_Address_AddressId",
                table: "School",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_Address_AddressId",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_School_AddressId",
                table: "School");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "School",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_School_AddressId",
                table: "School",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_School_Address_AddressId",
                table: "School",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
