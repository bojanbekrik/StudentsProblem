using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsProblem.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInAddressAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_School_AddressId",
                table: "School");

            migrationBuilder.CreateIndex(
                name: "IX_School_AddressId",
                table: "School",
                column: "AddressId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_School_AddressId",
                table: "School");

            migrationBuilder.CreateIndex(
                name: "IX_School_AddressId",
                table: "School",
                column: "AddressId");
        }
    }
}
