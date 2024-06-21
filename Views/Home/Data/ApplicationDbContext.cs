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
        public DbSet<LocalProductEntity> LocalProductEntity { get; set; }
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
                entity.HasKey(e => e.Id);
                entity.ToTable("QuoteEntity");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Amount).HasColumnType("money");
                entity.Property(e => e.DDate).HasColumnType("DateTime");
                entity.Property(e => e.Client).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.QDescription).HasMaxLength(1000).IsUnicode(false);
                entity.Property(e => e.Notes).HasMaxLength(500).IsUnicode(false);
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("CustomerEntity");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Client).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.LicenceType).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<LocalProductEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("LocalProductEntity");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Product).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.PDescription).HasMaxLength(1000).IsUnicode(false);
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("UserEntity");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.User).HasMaxLength(50).IsUnicode(false);
            });
        }
    }
}