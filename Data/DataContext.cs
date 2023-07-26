using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockAPI.Models;

namespace StockAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){

        }


    public DbSet<Product> Products { get; set; }
            public DbSet<Entree> Entrees { get; set; }
            public DbSet<Sortie> Sorties { get; set; }
            public DbSet<Stockage> Stockages { get; set; }
            public DbSet<Destockage> Destockages { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder){
                modelBuilder.Entity<Stockage>()
                    .HasKey(st => new {st.num_bon_liv, st.num_produit});
                modelBuilder.Entity<Stockage>()
                    .HasOne(st => st.Products)
                    .WithMany(st => st.Entrees)
                    .HasForeignKey(st => st.num_produit);
                modelBuilder.Entity<Stockage>()
                    .HasOne(st => st.Entrees)
                    .WithMany(st => st.Products)
                    .HasForeignKey(st => st.num_bon_liv);

                modelBuilder.Entity<Destockage>()
                    .HasKey(st => new {st.num_facture, st.num_produit});
                modelBuilder.Entity<Destockage>()
                    .HasOne(st => st.Products)
                    .WithMany(st => st.Sorties)
                    .HasForeignKey(st => st.num_produit);
                modelBuilder.Entity<Destockage>()
                    .HasOne(st => st.Sorties)
                    .WithMany(st => st.Products)
                    .HasForeignKey(st => st.num_facture);
            }

        }
}

