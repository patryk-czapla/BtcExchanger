using Microsoft.EntityFrameworkCore;

namespace dotnet_bitcoin.Models
{
    public class BTCContext : DbContext
    {
        public BTCContext(DbContextOptions<BTCContext> options)
            : base(options)
        {
        }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}