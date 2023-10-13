using Microsoft.EntityFrameworkCore;

namespace Profit.Models
{
    public class ProfitContext:DbContext
    {
        public ProfitContext(DbContextOptions<ProfitContext> options):base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
