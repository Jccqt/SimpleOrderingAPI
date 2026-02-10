using MySql.Data.MySqlClient;
using OrderingAPI.DTOs.OrderItemDTOs;
using OrderingAPI.Interfaces;
using OrderingAPI.Models;
using System.Data;

namespace OrderingAPI.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly string _connectionString;

        public OrderItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<OrderItems>> GetAllOrderItems()
        {
            List<OrderItems> orderItems = new List<OrderItems>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM order_items", conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                var orderItem = new OrderItems
                {
                    order_item_id = Convert.ToInt32(reader["order_item_id"]),
                    order_id = Convert.ToInt32(reader["order_id"]),
                    product_id = Convert.ToInt32(reader["product_id"]),
                    quantity = Convert.ToInt32(reader["quantity"]),
                    price = Convert.ToDecimal(reader["price"])
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }

        public async Task<List<OrderItems>> GetOrderItems(int id)
        {
            List<OrderItems> orderItems = new List<OrderItems>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM order_items WHERE order_id = @orderID", conn);
            cmd.Parameters.AddWithValue("@orderID", id);

            using var reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                var orderItem = new OrderItems
                {
                    order_item_id = Convert.ToInt32(reader["order_item_id"]),
                    order_id = Convert.ToInt32(reader["order_id"]),
                    product_id = Convert.ToInt32(reader["product_id"]),
                    quantity = Convert.ToInt32(reader["quantity"]),
                    price = Convert.ToDecimal(reader["price"])
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }

        public async Task<bool> AddOrderItem(AddOrderItemDTO orderItem)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("AddOrderItem", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_order_id", orderItem.OrderID);
            cmd.Parameters.AddWithValue("@p_product_id", orderItem.ProductID);
            cmd.Parameters.AddWithValue("@p_quantity", orderItem.Quantity);
            cmd.Parameters.AddWithValue("@p_price", orderItem.Price);

            var resultParam = new MySqlParameter("@p_result", MySqlDbType.Int32);
            resultParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(resultParam);

            await cmd.ExecuteNonQueryAsync();

            int result = Convert.ToInt32(resultParam.Value);

            return result == 1;
        }
    }
}
