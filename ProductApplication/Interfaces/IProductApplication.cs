using Infrastructure.Pagination;
using ProductDomain.DTO_s;
using ProductDomain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApplication.Interfaces
{
    public interface IProductApplication
    {
        Task AddProduct(ProductDTO product);
        Task<List<Product>> GetAllProducts(PageParamenters pageParamenters);
        Task<Product> GetProductById(Guid id);
        Task<List<Product>> GetByCategory(Category category);
        Task<Product> ChangeProduct(Guid id, ProductDTO product);
        Task<Product> DeleteProductById(Guid id);
    }
}
