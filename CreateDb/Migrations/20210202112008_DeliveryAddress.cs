using Microsoft.EntityFrameworkCore.Migrations;

namespace CreateDb.Migrations
{
    public partial class DeliveryAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerEntityId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerEntityId",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "OrderEntityId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrderEntityId",
                table: "Addresses",
                column: "OrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerEntityId",
                table: "Addresses",
                column: "CustomerEntityId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Orders_OrderEntityId",
                table: "Addresses",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerEntityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Orders_OrderEntityId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrderEntityId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerEntityId",
                table: "Addresses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerEntityId",
                table: "Addresses",
                column: "CustomerEntityId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
