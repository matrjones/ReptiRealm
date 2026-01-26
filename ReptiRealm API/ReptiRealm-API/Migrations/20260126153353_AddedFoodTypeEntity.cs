using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedFoodTypeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FoodTypeId",
                table: "Feed",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FoodType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feed_FoodTypeId",
                table: "Feed",
                column: "FoodTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_FoodType_FoodTypeId",
                table: "Feed",
                column: "FoodTypeId",
                principalTable: "FoodType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feed_FoodType_FoodTypeId",
                table: "Feed");

            migrationBuilder.DropTable(
                name: "FoodType");

            migrationBuilder.DropIndex(
                name: "IX_Feed_FoodTypeId",
                table: "Feed");

            migrationBuilder.DropColumn(
                name: "FoodTypeId",
                table: "Feed");
        }
    }
}
