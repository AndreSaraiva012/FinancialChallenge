using Infrastructure.Repository;
using ProductDomain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductData.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetAsync(Expression<Func<Product, bool>> predicate);
    }
}
