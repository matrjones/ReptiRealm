using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefecationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DefecationId",
                table: "Reptiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Defecation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defecation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reptiles_DefecationId",
                table: "Reptiles",
                column: "DefecationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Defecation_DefecationId",
                table: "Reptiles",
                column: "DefecationId",
                principalTable: "Defecation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Defecation_DefecationId",
                table: "Reptiles");

            migrationBuilder.DropTable(
                name: "Defecation");

            migrationBuilder.DropIndex(
                name: "IX_Reptiles_DefecationId",
                table: "Reptiles");

            migrationBuilder.DropColumn(
                name: "DefecationId",
                table: "Reptiles");
        }
    }
}
