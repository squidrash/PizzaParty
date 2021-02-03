using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CreateDb.Migrations
{
    public partial class AddressOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressOrderEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderEntityId = table.Column<int>(nullable: false),
                    AddressEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressOrderEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressOrderEntity_Addresses_AddressEntityId",
                        column: x => x.AddressEntityId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressOrderEntity_Orders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressOrderEntity_AddressEntityId",
                table: "AddressOrderEntity",
                column: "AddressEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressOrderEntity_OrderEntityId",
                table: "AddressOrderEntity",
                column: "OrderEntityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressOrderEntity");
        }
    }
}
