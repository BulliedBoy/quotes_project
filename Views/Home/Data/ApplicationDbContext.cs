using Microsoft.EntityFrameworkCore;

namespace quotes_project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }
    }

    public class Quote
    {
        public int id_quote { get; set; }
        public int id_customer { get; set; }
        public int id_producto { get; set; }
        public int id_user { get; set; }
        public int amount { get; set; }
        public int dDate { get; set; }
    }
}
