using Microsoft.EntityFrameworkCore;
using WebApplication3105.Models;

namespace WebApplication3105.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
