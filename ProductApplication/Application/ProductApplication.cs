using AutoMapper;
using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using ProductApplication.Interfaces;
using ProductData.Repository;
using ProductDomain.DTO_s;
using ProductDomain.Entities;
using ProductDomain.Validators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProductApplication.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductApplication(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddProduct(ProductDTO product)
        {
            var productMapper = _mapper.Map<Product>(product);

            var validator = new ProductValidators();
            var validationResult = validator.Validate(productMapper);

            if (validationResult.IsValid)
                await _productRepository.AddAsync(productMapper);
            else
            {
                var errorList = new ErrorMessage<ProductDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                    validationResult.Errors.ConvertAll(x => x.ErrorMessage), product);
                throw new Exception(ErrorList(errorList));
            }
        }
        public async Task<List<Product>> GetAllProducts(PageParamenters pageParamenters)
        {
            var result = await _productRepository.GetAllWithPaging(pageParamenters);

            if (result.Count == 0)
                throw new Exception(ErrorList(NotFoundMessage(new ProductDTO())));

            return result;
        }
        public async Task<Product> GetProductById(Guid id)
        {
            var result = await _productRepository.GetByIdAsync(id);

            if (result == null)
                throw new Exception(ErrorList(NotFoundMessage(new ProductDTO())));

            return result;
        }
        public async Task<List<Product>> GetByCategory(Category category)
        {
            var result = await _productRepository.GetListAsync(x => x.ProductCategory == category);

            if (result == null)
                throw new Exception(ErrorList(NotFoundMessage(new ProductDTO())));

            return result;
        }
        public async Task<Product> ChangeProduct(Guid id, ProductDTO product)
        {
            var productUpdate = await _productRepository.GetByIdAsync(id);

            if (productUpdate == null)
                throw new Exception(ErrorList(NotFoundMessage(product)));

            var productMapper = _mapper.Map<Product>(product);
            productMapper.Id = id;

            var validator = new ProductValidators();
            var validationResult = validator.Validate(productMapper);

            if (validationResult.IsValid)
                await _productRepository.UpdateAsync(productMapper);
            else
            {
                var errorList = new ErrorMessage<ProductDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                 validationResult.Errors.ConvertAll(x => x.ErrorMessage.ToString()), product);
                throw new Exception(ErrorList(errorList));
            }

            return productMapper;
        }
        public async Task<Product> DeleteProductById(Guid id)
        {
            var result = await _productRepository.GetByIdAsync(id);

            if (result == null)
                throw new Exception(ErrorList(NotFoundMessage(new ProductDTO())));

            await _productRepository.DeleteAsync(result);

            return result;
        }

        public string ErrorList(ErrorMessage<ProductDTO> error)
        {
            var errorList = "";

            foreach (var item in error.Message)
            {
                errorList += item.ToString();
            }
            return errorList;
        }
        public ErrorMessage<ProductDTO> NotFoundMessage(ProductDTO bankRecord)
        {
            var errorList = new List<string>();
            errorList.Add("In database doesn´t contain the data you want....");
            var error = new ErrorMessage<ProductDTO>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, bankRecord);
            return error;
        }
        public ErrorMessage<ProductDTO> BadRequestMessage(ProductDTO bankRecord, string message)
        {
            var errorList = new List<string>();
            errorList.Add(message);
            var error = new ErrorMessage<ProductDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errorList, bankRecord);
            return error;
        }

    }
}
