using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LadleMeThis.Migrations
{
    /// <inheritdoc />
    public partial class base_second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedRecipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedRecipes",
                columns: table => new
                {
                    SavedRecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateSaved = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedRecipes", x => x.SavedRecipeId);
                    table.ForeignKey(
                        name: "FK_SavedRecipes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedRecipes_RecipeId",
                table: "SavedRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedRecipes_UserId",
                table: "SavedRecipes",
                column: "UserId");
        }
    }
}
