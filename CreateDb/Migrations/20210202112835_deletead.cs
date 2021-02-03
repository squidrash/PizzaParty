using Microsoft.EntityFrameworkCore.Migrations;

namespace CreateDb.Migrations
{
    public partial class deletead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Orders_OrderEntityId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrderEntityId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderEntityId",
                table: "Addresses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrderEntityId",
                table: "Addresses",
                column: "OrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Orders_OrderEntityId",
                table: "Addresses",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
