using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ProductDomain.Entities;

namespace ProductData
{
    public class ProductContext : DataContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        DbSet<Product> Product { get; set; }
    }
}
