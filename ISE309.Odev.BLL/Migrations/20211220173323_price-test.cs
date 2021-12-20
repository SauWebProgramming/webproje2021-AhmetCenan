using Microsoft.EntityFrameworkCore.Migrations;

namespace ISE309.Odev.BLL.Migrations
{
    public partial class pricetest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductWeight");

            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.AddColumn<string>(
                name: "Weights",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weights",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    WeightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeightType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.WeightID);
                });

            migrationBuilder.CreateTable(
                name: "ProductWeight",
                columns: table => new
                {
                    ProductsProductID = table.Column<int>(type: "int", nullable: false),
                    WeightsWeightID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWeight", x => new { x.ProductsProductID, x.WeightsWeightID });
                    table.ForeignKey(
                        name: "FK_ProductWeight_Products_ProductsProductID",
                        column: x => x.ProductsProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWeight_Weights_WeightsWeightID",
                        column: x => x.WeightsWeightID,
                        principalTable: "Weights",
                        principalColumn: "WeightID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductWeight_WeightsWeightID",
                table: "ProductWeight",
                column: "WeightsWeightID");
        }
    }
}
