using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserIdToMorph : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Morphs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Morphs");
        }
    }
}
