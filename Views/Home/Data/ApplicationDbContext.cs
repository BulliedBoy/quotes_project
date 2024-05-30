using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using quotes_project.Views.Home.Data.Entities;

namespace quotes_project.Views.Home.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<CustomerEntity> CustomerEntity { get; set; }
        public DbSet<ProductEntity> ProductEntity { get; set; }
        public DbSet<QuoteEntity> QuoteEntity { get; set; }
        public DbSet<UserEntity> UserEntity { get; set; }

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
            modelBuilder.Entity<QuoteEntity>(entity =>
            {
                entity.HasKey(e => e.IdQuote);
                entity.ToTable("QuoteEntity");

                entity.Property(e => e.IdQuote).ValueGeneratedOnAdd();
                entity.Property(e => e.Amount).HasColumnType("money");
                entity.Property(e => e.DDate).HasColumnType("DateTime");
                entity.Property(e => e.CustomerName).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(e => e.IdCustomer);
                entity.ToTable("CustomerEntity");

                entity.Property(e => e.IdCustomer).ValueGeneratedOnAdd();
                entity.Property(e => e.CustomerName).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.CustomerType).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(e => e.IdProduct);
                entity.ToTable("ProductEntity");

                entity.Property(e => e.IdProduct).ValueGeneratedOnAdd();
                entity.Property(e => e.ProductName).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.IdUser);
                entity.ToTable("UserEntity");

                entity.Property(e => e.IdUser).ValueGeneratedOnAdd();
                entity.Property(e => e.Username).HasMaxLength(50).IsUnicode(false);
            });
        }
    }
}
