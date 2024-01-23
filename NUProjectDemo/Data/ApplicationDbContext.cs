using Microsoft.EntityFrameworkCore;
using NUProjectDemo.Models;

namespace NUProjectDemo.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Products> Products { get; set; }
    }
}
