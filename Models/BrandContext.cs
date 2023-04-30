using Microsoft.EntityFrameworkCore;

namespace CrudOperationsinNetCore.Models
{
    public class BrandContext : DbContext
    {
        public BrandContext(DbContextOptions<BrandContext> options) : base(options)
        {
            
        }

        public DbSet<Brand> Brandss { get; set; }
    }
}
