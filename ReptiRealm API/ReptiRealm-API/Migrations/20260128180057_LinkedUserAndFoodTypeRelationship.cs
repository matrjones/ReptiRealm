using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class LinkedUserAndFoodTypeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FoodTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodTypes_UserId",
                table: "FoodTypes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodTypes_AspNetUsers_UserId",
                table: "FoodTypes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodTypes_AspNetUsers_UserId",
                table: "FoodTypes");

            migrationBuilder.DropIndex(
                name: "IX_FoodTypes_UserId",
                table: "FoodTypes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FoodTypes");
        }
    }
}
