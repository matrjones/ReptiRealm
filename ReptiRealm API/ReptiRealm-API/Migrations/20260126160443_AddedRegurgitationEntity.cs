using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedRegurgitationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RegurgitationId",
                table: "Feed",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Regurgitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regurgitation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feed_RegurgitationId",
                table: "Feed",
                column: "RegurgitationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_Regurgitation_RegurgitationId",
                table: "Feed",
                column: "RegurgitationId",
                principalTable: "Regurgitation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feed_Regurgitation_RegurgitationId",
                table: "Feed");

            migrationBuilder.DropTable(
                name: "Regurgitation");

            migrationBuilder.DropIndex(
                name: "IX_Feed_RegurgitationId",
                table: "Feed");

            migrationBuilder.DropColumn(
                name: "RegurgitationId",
                table: "Feed");
        }
    }
}
