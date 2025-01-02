using Microsoft.EntityFrameworkCore;
using Service.Domain;

namespace Service.Persistence;

public class ApplicationContext : DbContext
{
    public DbSet<BuyerModel> Buyers { get; set; }
    public DbSet<OrderModel> Order { get; set; }
    
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        
        
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}