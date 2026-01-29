using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class FixedRegurgitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Regurgitations_RegurgitationId",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_RegurgitationId",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "Regurgitations");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reptiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Regurgitations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegurgitationId",
                table: "Feeds",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_RegurgitationId",
                table: "Feeds",
                column: "RegurgitationId",
                unique: true,
                filter: "[RegurgitationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_Regurgitations_RegurgitationId",
                table: "Feeds",
                column: "RegurgitationId",
                principalTable: "Regurgitations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Regurgitations_RegurgitationId",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_RegurgitationId",
                table: "Feeds");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reptiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Regurgitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeedId",
                table: "Regurgitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegurgitationId",
                table: "Feeds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_RegurgitationId",
                table: "Feeds",
                column: "RegurgitationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_Regurgitations_RegurgitationId",
                table: "Feeds",
                column: "RegurgitationId",
                principalTable: "Regurgitations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
