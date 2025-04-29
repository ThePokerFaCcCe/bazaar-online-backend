using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazaarOnline.Infra.Data.Migrations
{
    public partial class MG_Advertisement_Add_Price_OHSHITHEREWEGOAGAIN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "PriceType",
                schema: "bazaar",
                table: "Advertisements",
                type: "smallint",
                nullable: false,
                defaultValue: (short)1);

            migrationBuilder.AddColumn<long>(
                name: "PriceValue",
                schema: "bazaar",
                table: "Advertisements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceType",
                schema: "bazaar",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "PriceValue",
                schema: "bazaar",
                table: "Advertisements");
        }
    }
}
