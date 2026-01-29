using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class FixedDeleteBehaviours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Species_SpeciesId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Species_AspNetUsers_UserId",
                table: "Species");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Weights",
                type: "decimal(7,3)",
                precision: 7,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,3)",
                oldPrecision: 5,
                oldScale: 3);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Species",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reptiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SpeciesId",
                table: "Morphs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Morphs_SpeciesId",
                table: "Morphs",
                column: "SpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Morphs_Species_SpeciesId",
                table: "Morphs",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Species_SpeciesId",
                table: "Reptiles",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Species_AspNetUsers_UserId",
                table: "Species",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Morphs_Species_SpeciesId",
                table: "Morphs");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Species_SpeciesId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Species_AspNetUsers_UserId",
                table: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Morphs_SpeciesId",
                table: "Morphs");

            migrationBuilder.DropColumn(
                name: "SpeciesId",
                table: "Morphs");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Weights",
                type: "decimal(5,3)",
                precision: 5,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,3)",
                oldPrecision: 7,
                oldScale: 3);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Species",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reptiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Species_SpeciesId",
                table: "Reptiles",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Species_AspNetUsers_UserId",
                table: "Species",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
