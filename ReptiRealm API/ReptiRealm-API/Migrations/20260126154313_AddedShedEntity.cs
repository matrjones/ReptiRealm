using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedShedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShedId",
                table: "Reptiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Feed",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Shed",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shed", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reptiles_ShedId",
                table: "Reptiles",
                column: "ShedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Shed_ShedId",
                table: "Reptiles",
                column: "ShedId",
                principalTable: "Shed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Shed_ShedId",
                table: "Reptiles");

            migrationBuilder.DropTable(
                name: "Shed");

            migrationBuilder.DropIndex(
                name: "IX_Reptiles_ShedId",
                table: "Reptiles");

            migrationBuilder.DropColumn(
                name: "ShedId",
                table: "Reptiles");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Feed",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
