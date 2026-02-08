using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.DTOs.ProductDTOs;
using OrderingAPI.Models;
using OrderingAPI.Repositories;
using System.Data.Common;

namespace OrderingAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> GetProducts()
        {
            try
            {
                var result = await _repository.GetAllProducts();

                var products = result.Select(p => ProductsToProductsDTO(p));

                return Ok(products);
            }
            catch (DbException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured on database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
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
