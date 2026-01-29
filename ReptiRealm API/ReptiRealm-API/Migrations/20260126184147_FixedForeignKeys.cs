using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class FixedForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Defecations_DefecationId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Feeds_FeedId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Morphs_MorphId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Sheds_ShedId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Weights_WeightId",
                table: "Reptiles");

            migrationBuilder.DropIndex(
                name: "IX_Reptiles_DefecationId",
                table: "Reptiles");

            migrationBuilder.DropIndex(
                name: "IX_Reptiles_FeedId",
                table: "Reptiles");

            migrationBuilder.DropIndex(
                name: "IX_Reptiles_MorphId",
                table: "Reptiles");

            migrationBuilder.DropIndex(
                name: "IX_Reptiles_ShedId",
                table: "Reptiles");

            migrationBuilder.DropIndex(
                name: "IX_Reptiles_WeightId",
                table: "Reptiles");

            migrationBuilder.DropColumn(
                name: "DefecationId",
                table: "Reptiles");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "Reptiles");

            migrationBuilder.DropColumn(
                name: "MorphId",
                table: "Reptiles");

            migrationBuilder.DropColumn(
                name: "ShedId",
                table: "Reptiles");

            migrationBuilder.DropColumn(
                name: "WeightId",
                table: "Reptiles");

            migrationBuilder.AddColumn<Guid>(
                name: "ReptileId",
                table: "Weights",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReptileId",
                table: "Sheds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FeedId",
                table: "Regurgitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ReptileId",
                table: "Feeds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReptileId",
                table: "Defecations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MorphReptile",
                columns: table => new
                {
                    MorphsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReptilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MorphReptile", x => new { x.MorphsId, x.ReptilesId });
                    table.ForeignKey(
                        name: "FK_MorphReptile_Morphs_MorphsId",
                        column: x => x.MorphsId,
                        principalTable: "Morphs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MorphReptile_Reptiles_ReptilesId",
                        column: x => x.ReptilesId,
                        principalTable: "Reptiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weights_ReptileId",
                table: "Weights",
                column: "ReptileId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheds_ReptileId",
                table: "Sheds",
                column: "ReptileId");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_ReptileId",
                table: "Feeds",
                column: "ReptileId");

            migrationBuilder.CreateIndex(
                name: "IX_Defecations_ReptileId",
                table: "Defecations",
                column: "ReptileId");

            migrationBuilder.CreateIndex(
                name: "IX_MorphReptile_ReptilesId",
                table: "MorphReptile",
                column: "ReptilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defecations_Reptiles_ReptileId",
                table: "Defecations",
                column: "ReptileId",
                principalTable: "Reptiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_Reptiles_ReptileId",
                table: "Feeds",
                column: "ReptileId",
                principalTable: "Reptiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sheds_Reptiles_ReptileId",
                table: "Sheds",
                column: "ReptileId",
                principalTable: "Reptiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weights_Reptiles_ReptileId",
                table: "Weights",
                column: "ReptileId",
                principalTable: "Reptiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defecations_Reptiles_ReptileId",
                table: "Defecations");

            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Reptiles_ReptileId",
                table: "Feeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Sheds_Reptiles_ReptileId",
                table: "Sheds");

            migrationBuilder.DropForeignKey(
                name: "FK_Weights_Reptiles_ReptileId",
                table: "Weights");

            migrationBuilder.DropTable(
                name: "MorphReptile");

            migrationBuilder.DropIndex(
                name: "IX_Weights_ReptileId",
                table: "Weights");

            migrationBuilder.DropIndex(
                name: "IX_Sheds_ReptileId",
                table: "Sheds");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_ReptileId",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Defecations_ReptileId",
                table: "Defecations");

            migrationBuilder.DropColumn(
                name: "ReptileId",
                table: "Weights");

            migrationBuilder.DropColumn(
                name: "ReptileId",
                table: "Sheds");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "Regurgitations");

            migrationBuilder.DropColumn(
                name: "ReptileId",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "ReptileId",
                table: "Defecations");

            migrationBuilder.AddColumn<Guid>(
                name: "DefecationId",
                table: "Reptiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FeedId",
                table: "Reptiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MorphId",
                table: "Reptiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ShedId",
                table: "Reptiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WeightId",
                table: "Reptiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reptiles_DefecationId",
                table: "Reptiles",
                column: "DefecationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reptiles_FeedId",
                table: "Reptiles",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Reptiles_MorphId",
                table: "Reptiles",
                column: "MorphId");

            migrationBuilder.CreateIndex(
                name: "IX_Reptiles_ShedId",
                table: "Reptiles",
                column: "ShedId");

            migrationBuilder.CreateIndex(
                name: "IX_Reptiles_WeightId",
                table: "Reptiles",
                column: "WeightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Defecations_DefecationId",
                table: "Reptiles",
                column: "DefecationId",
                principalTable: "Defecations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Feeds_FeedId",
                table: "Reptiles",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Morphs_MorphId",
                table: "Reptiles",
                column: "MorphId",
                principalTable: "Morphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Sheds_ShedId",
                table: "Reptiles",
                column: "ShedId",
                principalTable: "Sheds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Weights_WeightId",
                table: "Reptiles",
                column: "WeightId",
                principalTable: "Weights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
