using Microsoft.EntityFrameworkCore.Migrations;

namespace CreateDb.Migrations
{
    public partial class AddressOrd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressOrderEntity_Addresses_AddressEntityId",
                table: "AddressOrderEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressOrderEntity_Orders_OrderEntityId",
                table: "AddressOrderEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Bascets_Menus_MenuEntityId",
                table: "Bascets");

            migrationBuilder.DropForeignKey(
                name: "FK_Bascets_Orders_OrderEntityId",
                table: "Bascets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bascets",
                table: "Bascets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressOrderEntity",
                table: "AddressOrderEntity");

            migrationBuilder.RenameTable(
                name: "Bascets",
                newName: "OrderMenuEntities");

            migrationBuilder.RenameTable(
                name: "AddressOrderEntity",
                newName: "AddressOrderEntities");

            migrationBuilder.RenameIndex(
                name: "IX_Bascets_OrderEntityId",
                table: "OrderMenuEntities",
                newName: "IX_OrderMenuEntities_OrderEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Bascets_MenuEntityId",
                table: "OrderMenuEntities",
                newName: "IX_OrderMenuEntities_MenuEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressOrderEntity_OrderEntityId",
                table: "AddressOrderEntities",
                newName: "IX_AddressOrderEntities_OrderEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressOrderEntity_AddressEntityId",
                table: "AddressOrderEntities",
                newName: "IX_AddressOrderEntities_AddressEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderMenuEntities",
                table: "OrderMenuEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressOrderEntities",
                table: "AddressOrderEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressOrderEntities_Addresses_AddressEntityId",
                table: "AddressOrderEntities",
                column: "AddressEntityId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressOrderEntities_Orders_OrderEntityId",
                table: "AddressOrderEntities",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMenuEntities_Menus_MenuEntityId",
                table: "OrderMenuEntities",
                column: "MenuEntityId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMenuEntities_Orders_OrderEntityId",
                table: "OrderMenuEntities",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressOrderEntities_Addresses_AddressEntityId",
                table: "AddressOrderEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressOrderEntities_Orders_OrderEntityId",
                table: "AddressOrderEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderMenuEntities_Menus_MenuEntityId",
                table: "OrderMenuEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderMenuEntities_Orders_OrderEntityId",
                table: "OrderMenuEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderMenuEntities",
                table: "OrderMenuEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressOrderEntities",
                table: "AddressOrderEntities");

            migrationBuilder.RenameTable(
                name: "OrderMenuEntities",
                newName: "Bascets");

            migrationBuilder.RenameTable(
                name: "AddressOrderEntities",
                newName: "AddressOrderEntity");

            migrationBuilder.RenameIndex(
                name: "IX_OrderMenuEntities_OrderEntityId",
                table: "Bascets",
                newName: "IX_Bascets_OrderEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderMenuEntities_MenuEntityId",
                table: "Bascets",
                newName: "IX_Bascets_MenuEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressOrderEntities_OrderEntityId",
                table: "AddressOrderEntity",
                newName: "IX_AddressOrderEntity_OrderEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressOrderEntities_AddressEntityId",
                table: "AddressOrderEntity",
                newName: "IX_AddressOrderEntity_AddressEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bascets",
                table: "Bascets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressOrderEntity",
                table: "AddressOrderEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressOrderEntity_Addresses_AddressEntityId",
                table: "AddressOrderEntity",
                column: "AddressEntityId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressOrderEntity_Orders_OrderEntityId",
                table: "AddressOrderEntity",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bascets_Menus_MenuEntityId",
                table: "Bascets",
                column: "MenuEntityId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bascets_Orders_OrderEntityId",
                table: "Bascets",
                column: "OrderEntityId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
