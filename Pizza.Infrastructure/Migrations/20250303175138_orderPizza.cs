using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizza.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class orderPizza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Orders_OrderId",
                table: "Pizzas");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_OrderId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Pizzas");

            migrationBuilder.CreateTable(
                name: "OrderPizzas",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PizzasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPizzas", x => new { x.OrderId, x.PizzasId });
                    table.ForeignKey(
                        name: "FK_OrderPizzas_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPizzas_Pizzas_PizzasId",
                        column: x => x.PizzasId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPizzas_PizzasId",
                table: "OrderPizzas",
                column: "PizzasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPizzas");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Pizzas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_OrderId",
                table: "Pizzas",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Orders_OrderId",
                table: "Pizzas",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
