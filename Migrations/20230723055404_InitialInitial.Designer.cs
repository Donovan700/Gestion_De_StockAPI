﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockAPI.Data;

#nullable disable

namespace StockAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230723055404_InitialInitial")]
    partial class InitialInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("StockAPI.Models.Destockage", b =>
                {
                    b.Property<string>("num_facture")
                        .HasColumnType("TEXT");

                    b.Property<string>("num_produit")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<int>("quantite_sortie")
                        .HasColumnType("INTEGER");

                    b.HasKey("num_facture", "num_produit");

                    b.HasIndex("num_produit");

                    b.ToTable("Destockages");
                });

            modelBuilder.Entity("StockAPI.Models.Entree", b =>
                {
                    b.Property<string>("num_bon_liv")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateEntree")
                        .HasColumnType("TEXT");

                    b.Property<string>("nom_fournisseur")
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.HasKey("num_bon_liv");

                    b.ToTable("Entrees");
                });

            modelBuilder.Entity("StockAPI.Models.Product", b =>
                {
                    b.Property<string>("num_produit")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("design")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("image")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("quantite")
                        .HasColumnType("INTEGER");

                    b.HasKey("num_produit");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("StockAPI.Models.Sortie", b =>
                {
                    b.Property<string>("num_facture")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateEntree")
                        .HasColumnType("TEXT");

                    b.Property<string>("nom_cli")
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.HasKey("num_facture");

                    b.ToTable("Sorties");
                });

            modelBuilder.Entity("StockAPI.Models.Stockage", b =>
                {
                    b.Property<string>("num_bon_liv")
                        .HasColumnType("TEXT");

                    b.Property<string>("num_produit")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<int>("quantite_entree")
                        .HasColumnType("INTEGER");

                    b.HasKey("num_bon_liv", "num_produit");

                    b.HasIndex("num_produit");

                    b.ToTable("Stockages");
                });

            modelBuilder.Entity("StockAPI.Models.Destockage", b =>
                {
                    b.HasOne("StockAPI.Models.Sortie", "Sorties")
                        .WithMany("Products")
                        .HasForeignKey("num_facture")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockAPI.Models.Product", "Products")
                        .WithMany("Sorties")
                        .HasForeignKey("num_produit")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Products");

                    b.Navigation("Sorties");
                });

            modelBuilder.Entity("StockAPI.Models.Stockage", b =>
                {
                    b.HasOne("StockAPI.Models.Entree", "Entrees")
                        .WithMany("Products")
                        .HasForeignKey("num_bon_liv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockAPI.Models.Product", "Products")
                        .WithMany("Entrees")
                        .HasForeignKey("num_produit")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entrees");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("StockAPI.Models.Entree", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("StockAPI.Models.Product", b =>
                {
                    b.Navigation("Entrees");

                    b.Navigation("Sorties");
                });

            modelBuilder.Entity("StockAPI.Models.Sortie", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
