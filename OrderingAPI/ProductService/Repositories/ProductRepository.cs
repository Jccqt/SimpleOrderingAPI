using MySql.Data.MySqlClient;
using OrderingAPI.Shared.Models.Responses;
using ProductService.DTOs.V1.ProductDTOs;
using ProductService.Interfaces;
using ProductService.Models;
using System.Data;

namespace ProductService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ServiceResponse<object>> GetAllProducts()
        {
            var response = new ServiceResponse<object>();
            List<Products> products = new List<Products>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM products", conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                Products product = new Products
                {
                    product_id = Convert.ToInt32(reader["product_id"]),
                    product_name = reader["product_name"].ToString(),
                    price = Convert.ToDecimal(reader["price"]),
                    stock = Convert.ToInt32(reader["stock"])
                };

                products.Add(product);
            }

            if(products.Count > 0)
            {
                response.Success = true;
                response.Message = "Products found.";
                response.Data = products;
            }
            else
            {
                response.Message = "No product found.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetProduct(int productID)
        {
            var response = new ServiceResponse<object>();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM products WHERE product_id = @productID", conn);
            cmd.Parameters.AddWithValue("@productID", productID);
            
            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                var product = new Products
                {
                    product_id = Convert.ToInt32(reader["product_id"]),
                    product_name = reader["product_name"].ToString(),
                    price = Convert.ToDecimal(reader["price"]),
                    stock = Convert.ToInt32(reader["stock"])
                };

                response.Success = true;
                response.Message = "Product found.";
                response.Data = product;
            }
            else
            {
                response.Message = "No product found.";
            }

            return response;
        }

        public async Task<ServiceResponse> AddProduct(AddProductModel product)
        {
            var response = new ServiceResponse();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("AddProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_product_name", product.ProductName);
            cmd.Parameters.AddWithValue("@p_price", product.Price);
            cmd.Parameters.AddWithValue("@p_stock", product.Stock);

            int affectedRow = await cmd.ExecuteNonQueryAsync();

            if(affectedRow > 0)
            {
                response.Success = true;
                response.Message = "Product added successfully.";
            }
            else
            {
                response.Message = "Failed to add product.";
            }

            return response;
        }

        public async Task<bool> UpdateProduct(int productID, UpdateProductModel product)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("UpdateProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_product_id", productID);
            cmd.Parameters.AddWithValue("@p_product_name", product.ProductName);
            cmd.Parameters.AddWithValue("@p_price", product.Price);
            cmd.Parameters.AddWithValue("@p_stock", product.Stock);

            int rowAffected = await cmd.ExecuteNonQueryAsync();

            return rowAffected > 0;
        }
    } 
}
