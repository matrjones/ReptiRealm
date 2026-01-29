using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedReptileDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Species_SpeciesId",
                table: "Reptiles");

            migrationBuilder.RenameColumn(
                name: "DoB",
                table: "Reptiles",
                newName: "DateOfBirth");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpeciesId",
                table: "Reptiles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Species_SpeciesId",
                table: "Reptiles",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Species_SpeciesId",
                table: "Reptiles");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Reptiles",
                newName: "DoB");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpeciesId",
                table: "Reptiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Species_SpeciesId",
                table: "Reptiles",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
