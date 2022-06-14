using Infrastructure;
using Infrastructure.Repository;
using ProductDomain.Entities;

namespace ProductData.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(ProductContext context) : base(context)
        {
            _context = context;
        }
    }
}
