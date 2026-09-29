using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddingSizeToFoodType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FoodTypes",
                newName: "Size");

            migrationBuilder.AddColumn<string>(
                name: "AnimalType",
                table: "FoodTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalType",
                table: "FoodTypes");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "FoodTypes",
                newName: "Name");
        }
    }
}
