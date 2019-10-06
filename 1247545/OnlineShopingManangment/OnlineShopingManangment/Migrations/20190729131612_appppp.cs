using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopingManangment.Migrations
{
    public partial class appppp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShoppingCartId",
                table: "shoppingCartItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "shoppingCarts",
                columns: table => new
                {
                    ShoppingCartId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCarts", x => x.ShoppingCartId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCartItems_ShoppingCartId",
                table: "shoppingCartItems",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_shoppingCartItems_shoppingCarts_ShoppingCartId",
                table: "shoppingCartItems",
                column: "ShoppingCartId",
                principalTable: "shoppingCarts",
                principalColumn: "ShoppingCartId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shoppingCartItems_shoppingCarts_ShoppingCartId",
                table: "shoppingCartItems");

            migrationBuilder.DropTable(
                name: "shoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_shoppingCartItems_ShoppingCartId",
                table: "shoppingCartItems");

            migrationBuilder.AlterColumn<string>(
                name: "ShoppingCartId",
                table: "shoppingCartItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
