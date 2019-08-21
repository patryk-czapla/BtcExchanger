using Microsoft.EntityFrameworkCore;

namespace BtcExchanger.Models
{
    public class BTCContext : DbContext
    {
        public BTCContext(DbContextOptions<BTCContext> options)
            : base(options)
        {
        }      
        public DbSet<Transaction> TransactionItems { get; set; }
        public DbSet<Verification> VerificationItems { get; set; }
    }
}