using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Repositories;
using System.Data.Common;
using Asp.Versioning;
using ProductService.DTOs.V1.ProductDTOs;
using Microsoft.AspNetCore.RateLimiting;
using OrderingAPI.Shared.Models.Responses;
using System.Data.OleDb;

namespace ProductService.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/v1/products
        [HttpGet]
        public async Task<ActionResult<ServiceResponse>> GetProducts()
        {
            var result = await _repository.GetAllProducts();

            return result.Success ? Ok(result) : NotFound(result);
        }

        // GET: api/v1/products?id={}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetProduct(int productID)
        {
            if(productID <= 0)
            {
                return BadRequest(new ServiceResponse { Message = "Invalid product ID." });
            }

            var result = await _repository.GetProduct(productID);

            return result.Success ? Ok(result) : NotFound(result);
        }

        // POST: api/v1/products
        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse>> AddProduct(AddProductDTO product)
        {
            if (product == null)
            {
                return BadRequest(new ServiceResponse { Message = "Invalid product data." });
            }

            var result = await _repository.AddProduct(AddProductMapper(product));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        // PUT: api/v1/products?id={}
        [HttpPut("{productID}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse>> UpdateProduct(int productID, UpdateProductDTO product)
        {
            if (product == null)
            {
                return BadRequest(new ServiceResponse { Message = "Invalid product data." });
            }

            if(productID <= 0)
            {
                return BadRequest(new ServiceResponse { Message = "Invalid product ID." });
            }

            var result = await _repository.UpdateProduct(productID, UpdateProductMapper(product));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        private ProductsDTO ProductsToProductsDTO(Products products) =>
            new ProductsDTO
            {
                ProductID = products.product_id,
                ProductName = products.product_name,
                Price = products.price,
                Stock = products.stock
            };

        private AddProductModel AddProductMapper(AddProductDTO product) =>
            new AddProductModel
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Stock = product.Stock
            };

        private UpdateProductModel UpdateProductMapper(UpdateProductDTO product) =>
            new UpdateProductModel
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Stock = product.Stock
            };
    }
}
