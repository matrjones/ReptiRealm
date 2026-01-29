using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotesToSpeciesAndMorphs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Species",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Species",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Species_UserId",
                table: "Species",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Species_AspNetUsers_UserId",
                table: "Species",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Species_AspNetUsers_UserId",
                table: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Species_UserId",
                table: "Species");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Species");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Species");
        }
    }
}
