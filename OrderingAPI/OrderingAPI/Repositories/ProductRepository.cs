using MySql.Data.MySqlClient;
using OrderingAPI.Models;

namespace OrderingAPI.Repositories
{
    public class ProductRepository
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

            using var reader = cmd.ExecuteReader();

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

        public async Task<Products> GetProduct(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM products WHERE product_id = @productID", conn);
            cmd.Parameters.AddWithValue("@productID", id);
            
            using var reader = cmd.ExecuteReader();

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

        public async Task AddProduct(Products product)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("CALL AddProduct(@productName, @price, @stock)", conn);
            cmd.Parameters.AddWithValue("@productName", product.product_name);
            cmd.Parameters.AddWithValue("@price", product.price);
            cmd.Parameters.AddWithValue("@stock", product.stock);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
