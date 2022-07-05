using Microsoft.EntityFrameworkCore;
using WebAppHangfire.Entities;

namespace WebAppHangfire
{

    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Product> Products { get; set; }
    }
}
