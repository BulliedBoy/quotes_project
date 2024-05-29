﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using quotes_project.Views.Home.Data;
using quotes_project.Models;
using quotes_project.Views.Home.Data.Entities;

namespace quotes_project.Views.Home.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<CustomerEntity> CustomerEntity { get; set; }
        public virtual DbSet<ProductEntity> ProductEntity { get; set; }
        public virtual DbSet<QuoteEntity> QuoteEntity { get; set; }
        public virtual DbSet<UserEntity> UserEntity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(e => e.IdCustomer);

                entity.ToTable("CustomerEntity");

                entity.Property(e => e.IdCustomer)
                    .ValueGeneratedNever()
                    .HasColumnName("id_customer");
                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("customer_name");
            });

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("ProductEntity");

                entity.Property(e => e.IdProduct)
                    .ValueGeneratedNever()
                    .HasColumnName("id_Product");
                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_name");
            });

            modelBuilder.Entity<QuoteEntity>(entity =>
            {
                entity.HasKey(e => e.IdQuote);

                entity.ToTable("QuoteEntity");

                entity.Property(e => e.IdQuote)
                    .ValueGeneratedNever()
                    .HasColumnName("id_quote");
                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");
                entity.Property(e => e.DDate).HasColumnName("dDate");
                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");
                entity.Property(e => e.IdProduct).HasColumnName("id_product");
                entity.Property(e => e.IdUser).HasColumnName("id_user");
                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("customername");
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("UserEntity");

                entity.Property(e => e.IdUser)
                    .ValueGeneratedNever()
                    .HasColumnName("id_user");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}