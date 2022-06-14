using BuyRequestData.Configuration;
using BuyRequestDomain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DataBuyRequest
{
    public class BuyRequestContext : DataContext
    {
        public BuyRequestContext(DbContextOptions<BuyRequestContext> options) : base(options)
        {

        }

        public DbSet<BuyRequest> Requests { get; set; }
        public DbSet<ProductRequest> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BuyRequestConfiguration());
            modelBuilder.ApplyConfiguration(new ProductRequestConfiguration());
        }
    }
}
