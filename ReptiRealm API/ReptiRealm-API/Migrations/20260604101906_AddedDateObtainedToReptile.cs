using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateObtainedToReptile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateObtained",
                table: "Reptiles",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateObtained",
                table: "Reptiles");
        }
    }
}
