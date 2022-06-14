using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.Interfaces;
using ProductDomain.DTO_s;
using ProductDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDTO product)
        {
            try
            {
                await _productApplication.AddProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<ProductDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errorList, product));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageParamenters paramenters)
        {
            try
            {
                return Ok(await _productApplication.GetAllProducts(paramenters));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<ProductDTO>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, new ProductDTO()));
            }
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetById([Required] Guid id)
        {

            try
            {
                var product = await _productApplication.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<ProductDTO>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, new ProductDTO()));
            }
        }

        [HttpGet("GetByProductCategory")]
        public async Task<IActionResult> GetByProductCategory(Category category)
        {

            try
            {
                return Ok(await _productApplication.GetByCategory(category));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<ProductDTO>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, new ProductDTO()));
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangeProduct(Guid Id, [FromBody] ProductDTO bankRecord)
        {
            try
            {
                return Ok(await _productApplication.ChangeProduct(Id, bankRecord));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<ProductDTO>(HttpStatusCode.BadRequest.GetHashCode().
                     ToString(), errorList, new ProductDTO()));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            try
            {
                var result = await _productApplication.DeleteProductById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<ProductDTO>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new ProductDTO()));
            }
        }
    }
}
