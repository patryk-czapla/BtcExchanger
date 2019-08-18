using Microsoft.EntityFrameworkCore;

namespace BtcExchanger.Models
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