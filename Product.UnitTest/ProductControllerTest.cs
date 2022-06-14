using Infrastructure.Pagination;
using Moq;
using Moq.AutoMock;
using Product.UnitTest.AutoFaker;
using ProductAPI.Controllers;
using ProductApplication.Interfaces;
using ProductDomain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Product.UnitTest
{
    public class ProductControllerTest
    {
        private readonly AutoMocker _autoMocker;

        public ProductControllerTest()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact(DisplayName = "Testing Adding Product")]
        public async Task TestAddProduct()
        {
            var product = ProductAutoFaker.GeneratProductDTO();

            var productApplication = _autoMocker.GetMock<IProductApplication>();
            productApplication.Setup(x => x.AddProduct(product));

            var controller = _autoMocker.CreateInstance<ProductController>();
            await controller.Post(product);

            productApplication.Verify(x => x.AddProduct(It.IsAny<ProductDTO>()), Times.Once);
        }

        [Fact(DisplayName = "Testing GetAll Products")]
        public async Task TestGetAllProducts()
        {
            var productApplication = _autoMocker.GetMock<IProductApplication>();
            PageParamenters paramenters = new PageParamenters();

            productApplication.Setup(x => x.GetAllProducts(paramenters));

            var controller = _autoMocker.CreateInstance<ProductController>();
            await controller.GetAll(paramenters);

            productApplication.Verify(x => x.GetAllProducts(paramenters), Times.Once);
        }

        [Fact(DisplayName = "Testing GetByProductId")]
        public async Task TestGetByProductId()
        {
            var productApplication = _autoMocker.GetMock<IProductApplication>();
            var id = Guid.NewGuid();

            productApplication.Setup(x => x.GetProductById(id));

            var controller = _autoMocker.CreateInstance<ProductController>();
            await controller.GetById(id);

            productApplication.Verify(x => x.GetProductById(id), Times.Once);
        }

        [Fact(DisplayName = "Testing GetByCategory")]
        public async Task TestGetByCategory()
        {
            var productApplication = _autoMocker.GetMock<IProductApplication>();
            var product = ProductAutoFaker.GeneratProductDTO();

            productApplication.Setup(x => x.GetByCategory(product.ProductCategory));

            var controller = _autoMocker.CreateInstance<ProductController>();
            await controller.GetByProductCategory(product.ProductCategory);

            productApplication.Verify(x => x.GetByCategory(product.ProductCategory), Times.Once);
        }

        [Fact(DisplayName = "Testing Update Product")]
        public async Task TestUpdateProduct()
        {
            var id = Guid.NewGuid();
            var product = ProductAutoFaker.GeneratProductDTO();

            var productApplication = _autoMocker.GetMock<IProductApplication>();
            productApplication.Setup(x => x.GetProductById(id));

            var controller = _autoMocker.CreateInstance<ProductController>();
            await controller.ChangeProduct(id, product);

            productApplication.Verify(x => x.ChangeProduct(id, product), Times.Once);
        }

        [Fact(DisplayName = "Testing Delete Product")]
        public async Task TestDeleteProduct()
        {
            var id = Guid.NewGuid();

            var productApplication = _autoMocker.GetMock<IProductApplication>();

            productApplication.Setup(x => x.DeleteProductById(id));

            var controller = _autoMocker.CreateInstance<ProductController>();
            await controller.DeleteById(id);

            productApplication.Verify(x => x.DeleteProductById(id), Times.Once());
        }
    }
}
