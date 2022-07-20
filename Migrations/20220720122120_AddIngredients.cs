using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_razor_layout.Migrations
{
    public partial class AddIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientPizza",
                columns: table => new
                {
                    ingredientsId = table.Column<int>(type: "int", nullable: false),
                    pizzasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPizza", x => new { x.ingredientsId, x.pizzasId });
                    table.ForeignKey(
                        name: "FK_IngredientPizza_ingredient_ingredientsId",
                        column: x => x.ingredientsId,
                        principalTable: "ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientPizza_pizza_pizzasId",
                        column: x => x.pizzasId,
                        principalTable: "pizza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPizza_pizzasId",
                table: "IngredientPizza",
                column: "pizzasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientPizza");

            migrationBuilder.DropTable(
                name: "ingredient");
        }
    }
}
