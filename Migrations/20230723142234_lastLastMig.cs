using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAPI.Migrations
{
    /// <inheritdoc />
    public partial class lastLastMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdStockage",
                table: "Stockages");

            migrationBuilder.DropColumn(
                name: "IdDestockage",
                table: "Destockages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdStockage",
                table: "Stockages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDestockage",
                table: "Destockages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
