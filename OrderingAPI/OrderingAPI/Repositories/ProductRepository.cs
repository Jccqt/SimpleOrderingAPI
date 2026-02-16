using MySql.Data.MySqlClient;
using OrderingAPI.DTOs.ProductDTOs;
using OrderingAPI.Interfaces;
using OrderingAPI.Models;
using System.Data;

namespace OrderingAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Products>> GetAllProducts()
        {
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

            return products;
        }

        public async Task<Products> GetProduct(int productID)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM products WHERE product_id = @productID", conn);
            cmd.Parameters.AddWithValue("@productID", productID);
            
            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                return new Products
                {
                    product_id = Convert.ToInt32(reader["product_id"]),
                    product_name = reader["product_name"].ToString(),
                    price = Convert.ToDecimal(reader["price"]),
                    stock = Convert.ToInt32(reader["stock"])
                };
            }

            return null;
        }

        public async Task AddProduct(AddProductDTO product)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("AddProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_product_name", product.ProductName);
            cmd.Parameters.AddWithValue("@p_price", product.Price);
            cmd.Parameters.AddWithValue("@p_stock", product.Stock);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> UpdateProduct(int productID, UpdateProductDTO product)
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
