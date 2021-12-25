using Microsoft.EntityFrameworkCore.Migrations;

namespace ISE309.Odev.BLL.Migrations
{
    public partial class ordermig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Restaurants_RestaurantID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RestaurantID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RestaurantID",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantID",
                table: "Orders",
                column: "RestaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Restaurants_RestaurantID",
                table: "Orders",
                column: "RestaurantID",
                principalTable: "Restaurants",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
