using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRequiredFieldFromNameToDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Feeds");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Feeds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Feeds");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Feeds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
