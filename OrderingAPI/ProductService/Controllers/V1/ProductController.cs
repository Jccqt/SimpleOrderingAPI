using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Repositories;
using OrderingAPI.Shared.Models;
using System.Data.Common;
using ProductService.DTOs.V1.ProductDTOs.ProductDTOs;
using Asp.Versioning;

namespace ProductService.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [ApiController]
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
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductsDTO>>>> GetProducts()
        {
            var result = await _repository.GetAllProducts();

            var products = result.Select(p => ProductsToProductsDTO(p));

            var response = new ServiceResponse<IEnumerable<ProductsDTO>>
            {
                Success = true,
                Message = "Products retrieved successfully!",
                Data = products
            };

            return Ok(products);
        }

        // GET: api/v1/products?id={}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ProductsDTO>>> GetProduct(int productID)
        {
            var product = await _repository.GetProduct(productID);

            if (product == null)
            {
                return NotFound(new ServiceResponse<ProductsDTO>
                {
                    Success = false,
                    Message = "Product not found."
                });
            }

            var response = new ServiceResponse<ProductsDTO>
            {
                Success = true,
                Message = "Product retrieved successfully!",
                Data = ProductsToProductsDTO(product)
            };

            return Ok(response);
        }

        // POST: api/v1/products
        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<AddProductDTO>>> AddProduct(AddProductDTO product)
        {
            if (product == null)
            {
                return BadRequest(new ServiceResponse<AddProductDTO>
                {
                    Success = false,
                    Message = "Invalid product data."
                });
            }

            await _repository.AddProduct(product);

            var response = new ServiceResponse<AddProductDTO>
            {
                Success = true,
                Message = "Product added successfully!",
                Data = product
            };

            return Ok(response);
        }

        // PUT: api/v1/products?id={}
        [HttpPut("{productID}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<UpdateProductDTO>>> UpdateProduct(int productID, UpdateProductDTO product)
        {
            if (product == null)
            {
                return BadRequest(new ServiceResponse<UpdateProductDTO>
                {
                    Success = false,
                    Message = "Invalid product data."
                });
            }

            bool result = await _repository.UpdateProduct(productID, product);

            if (!result)
            {
                return NotFound(new ServiceResponse<UpdateProductDTO>
                {
                    Success = false,
                    Message = "Product not found."
                });
            }

            var response = new ServiceResponse<UpdateProductDTO>
            {
                Success = true,
                Message = "Product updated successfully!",
                Data = product
            };

            return Ok(response);
        }

        private ProductsDTO ProductsToProductsDTO(Products products) =>
            new ProductsDTO
            {
                ProductID = products.product_id,
                ProductName = products.product_name,
                Price = products.price,
                Stock = products.stock
            };
    }
}
