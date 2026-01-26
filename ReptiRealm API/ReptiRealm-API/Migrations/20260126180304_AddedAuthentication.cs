using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReptiRealm_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feed_FoodType_FoodTypeId",
                table: "Feed");

            migrationBuilder.DropForeignKey(
                name: "FK_Feed_Regurgitation_RegurgitationId",
                table: "Feed");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Defecation_DefecationId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Feed_FeedId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Morph_MorphId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Shed_ShedId",
                table: "Reptiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_Weight_WeightId",
                table: "Reptiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weight",
                table: "Weight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shed",
                table: "Shed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regurgitation",
                table: "Regurgitation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Morph",
                table: "Morph");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodType",
                table: "FoodType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feed",
                table: "Feed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Defecation",
                table: "Defecation");

            migrationBuilder.RenameTable(
                name: "Weight",
                newName: "Weights");

            migrationBuilder.RenameTable(
                name: "Shed",
                newName: "Sheds");

            migrationBuilder.RenameTable(
                name: "Regurgitation",
                newName: "Regurgitations");

            migrationBuilder.RenameTable(
                name: "Morph",
                newName: "Morphs");

            migrationBuilder.RenameTable(
                name: "FoodType",
                newName: "FoodTypes");

            migrationBuilder.RenameTable(
                name: "Feed",
                newName: "Feeds");

            migrationBuilder.RenameTable(
                name: "Defecation",
                newName: "Defecations");

            migrationBuilder.RenameIndex(
                name: "IX_Feed_RegurgitationId",
                table: "Feeds",
                newName: "IX_Feeds_RegurgitationId");

            migrationBuilder.RenameIndex(
                name: "IX_Feed_FoodTypeId",
                table: "Feeds",
                newName: "IX_Feeds_FoodTypeId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reptiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Weights",
                type: "decimal(5,3)",
                precision: 5,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weights",
                table: "Weights",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sheds",
                table: "Sheds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regurgitations",
                table: "Regurgitations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Morphs",
                table: "Morphs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodTypes",
                table: "FoodTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feeds",
                table: "Feeds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Defecations",
                table: "Defecations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reptiles_UserId",
                table: "Reptiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_FoodTypes_FoodTypeId",
                table: "Feeds",
                column: "FoodTypeId",
                principalTable: "FoodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_Regurgitations_RegurgitationId",
                table: "Feeds",
                column: "RegurgitationId",
                principalTable: "Regurgitations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_AspNetUsers_UserId",
                table: "Reptiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_FoodTypes_FoodTypeId",
                table: "Feeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Regurgitations_RegurgitationId",
                table: "Feeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Reptiles_AspNetUsers_UserId",
                table: "Reptiles");

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
                name: "IX_Reptiles_UserId",
                table: "Reptiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weights",
                table: "Weights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sheds",
                table: "Sheds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regurgitations",
                table: "Regurgitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Morphs",
                table: "Morphs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodTypes",
                table: "FoodTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feeds",
                table: "Feeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Defecations",
                table: "Defecations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reptiles");

            migrationBuilder.RenameTable(
                name: "Weights",
                newName: "Weight");

            migrationBuilder.RenameTable(
                name: "Sheds",
                newName: "Shed");

            migrationBuilder.RenameTable(
                name: "Regurgitations",
                newName: "Regurgitation");

            migrationBuilder.RenameTable(
                name: "Morphs",
                newName: "Morph");

            migrationBuilder.RenameTable(
                name: "FoodTypes",
                newName: "FoodType");

            migrationBuilder.RenameTable(
                name: "Feeds",
                newName: "Feed");

            migrationBuilder.RenameTable(
                name: "Defecations",
                newName: "Defecation");

            migrationBuilder.RenameIndex(
                name: "IX_Feeds_RegurgitationId",
                table: "Feed",
                newName: "IX_Feed_RegurgitationId");

            migrationBuilder.RenameIndex(
                name: "IX_Feeds_FoodTypeId",
                table: "Feed",
                newName: "IX_Feed_FoodTypeId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Weight",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,3)",
                oldPrecision: 5,
                oldScale: 3);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weight",
                table: "Weight",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shed",
                table: "Shed",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regurgitation",
                table: "Regurgitation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Morph",
                table: "Morph",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodType",
                table: "FoodType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feed",
                table: "Feed",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Defecation",
                table: "Defecation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_FoodType_FoodTypeId",
                table: "Feed",
                column: "FoodTypeId",
                principalTable: "FoodType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_Regurgitation_RegurgitationId",
                table: "Feed",
                column: "RegurgitationId",
                principalTable: "Regurgitation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Defecation_DefecationId",
                table: "Reptiles",
                column: "DefecationId",
                principalTable: "Defecation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Feed_FeedId",
                table: "Reptiles",
                column: "FeedId",
                principalTable: "Feed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Morph_MorphId",
                table: "Reptiles",
                column: "MorphId",
                principalTable: "Morph",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Shed_ShedId",
                table: "Reptiles",
                column: "ShedId",
                principalTable: "Shed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reptiles_Weight_WeightId",
                table: "Reptiles",
                column: "WeightId",
                principalTable: "Weight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
