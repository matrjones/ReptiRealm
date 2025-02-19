using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm.Migrations
{
    /// <inheritdoc />
    public partial class FixedRegurgitationForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regurgitations_Feeds_FeedId",
                table: "Regurgitations");

            migrationBuilder.DropIndex(
                name: "IX_Regurgitations_FeedId",
                table: "Regurgitations");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "Regurgitations");

            migrationBuilder.AddColumn<Guid>(
                name: "RegurgitationId",
                table: "Feeds",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_RegurgitationId",
                table: "Feeds",
                column: "RegurgitationId");

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

            migrationBuilder.DropColumn(
                name: "RegurgitationId",
                table: "Feeds");

            migrationBuilder.AddColumn<Guid>(
                name: "FeedId",
                table: "Regurgitations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Regurgitations_FeedId",
                table: "Regurgitations",
                column: "FeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regurgitations_Feeds_FeedId",
                table: "Regurgitations",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
