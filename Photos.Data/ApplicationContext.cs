using Microsoft.EntityFrameworkCore;
using Photos.Data.Models;

namespace Photos.Data
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Image> Images { get; set; }
    }
}
