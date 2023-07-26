using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entrees",
                columns: table => new
                {
                    num_bon_liv = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    DateEntree = table.Column<DateTime>(type: "TEXT", nullable: false),
                    nom_fournisseur = table.Column<string>(type: "TEXT", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrees", x => x.num_bon_liv);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    num_produit = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    design = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    quantite = table.Column<int>(type: "INTEGER", nullable: false),
                    image = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.num_produit);
                });

            migrationBuilder.CreateTable(
                name: "Sorties",
                columns: table => new
                {
                    num_facture = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    DateEntree = table.Column<DateTime>(type: "TEXT", nullable: false),
                    nom_cli = table.Column<string>(type: "TEXT", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorties", x => x.num_facture);
                });

            migrationBuilder.CreateTable(
                name: "Stockages",
                columns: table => new
                {
                    num_produit = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    num_bon_liv = table.Column<string>(type: "TEXT", nullable: false),
                    quantite_entree = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stockages", x => new { x.num_bon_liv, x.num_produit });
                    table.ForeignKey(
                        name: "FK_Stockages_Entrees_num_bon_liv",
                        column: x => x.num_bon_liv,
                        principalTable: "Entrees",
                        principalColumn: "num_bon_liv",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stockages_Products_num_produit",
                        column: x => x.num_produit,
                        principalTable: "Products",
                        principalColumn: "num_produit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Destockages",
                columns: table => new
                {
                    num_produit = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    num_facture = table.Column<string>(type: "TEXT", nullable: false),
                    quantite_sortie = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destockages", x => new { x.num_facture, x.num_produit });
                    table.ForeignKey(
                        name: "FK_Destockages_Products_num_produit",
                        column: x => x.num_produit,
                        principalTable: "Products",
                        principalColumn: "num_produit",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Destockages_Sorties_num_facture",
                        column: x => x.num_facture,
                        principalTable: "Sorties",
                        principalColumn: "num_facture",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destockages_num_produit",
                table: "Destockages",
                column: "num_produit");

            migrationBuilder.CreateIndex(
                name: "IX_Stockages_num_produit",
                table: "Stockages",
                column: "num_produit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Destockages");

            migrationBuilder.DropTable(
                name: "Stockages");

            migrationBuilder.DropTable(
                name: "Sorties");

            migrationBuilder.DropTable(
                name: "Entrees");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
