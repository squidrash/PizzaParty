using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CreateDb.Migrations
{
    public partial class CustomerAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerEntityId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerEntityId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CustomerEntityId",
                table: "Addresses");

            migrationBuilder.CreateTable(
                name: "CustomerAddressEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerEntityId = table.Column<int>(nullable: false),
                    AddressEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddressEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddressEntities_Addresses_AddressEntityId",
                        column: x => x.AddressEntityId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAddressEntities_Customers_CustomerEntityId",
                        column: x => x.CustomerEntityId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddressEntities_AddressEntityId",
                table: "CustomerAddressEntities",
                column: "AddressEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddressEntities_CustomerEntityId",
                table: "CustomerAddressEntities",
                column: "CustomerEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddressEntities");

            migrationBuilder.AddColumn<int>(
                name: "CustomerEntityId",
                table: "Addresses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerEntityId",
                table: "Addresses",
                column: "CustomerEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerEntityId",
                table: "Addresses",
                column: "CustomerEntityId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
