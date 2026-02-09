using MySql.Data.MySqlClient;
using OrderingAPI.DTOs.OrderDTOs;
using OrderingAPI.Models;
using System.Data;

namespace OrderingAPI.Repositories
{
    public class OrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Orders>> GetAllOrders()
        {
            List<Orders> orders = new List<Orders>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM orders", conn);
            
            using var reader = cmd.ExecuteReader();

            while(await reader.ReadAsync())
            {
                Orders order = new Orders
                {
                    order_id = Convert.ToInt32(reader["order_id"]),
                    user_id = Convert.ToInt32(reader["user_id"]),
                    order_date = Convert.ToDateTime(reader["order_date"]),
                    status = reader["status"].ToString()
                };

                orders.Add(order);
            }

            return orders;
        }

        public async Task<Orders> GetOrder(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM orders WHERE order_id = @orderID", conn);
            cmd.Parameters.AddWithValue("@orderID", id);

            using var reader = cmd.ExecuteReader();

            if(await reader.ReadAsync())
            {
                return new Orders
                {
                    order_id = Convert.ToInt32(reader["order_id"]),
                    user_id = Convert.ToInt32(reader["user_id"]),
                    order_date = Convert.ToDateTime(reader["order_date"]),
                    status = reader["status"].ToString()
                };
            }

            return null;
        }

        public async Task<bool> AddOrder(AddOrderDTO order)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("AddOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_user_id", order.UserID);
            cmd.Parameters.AddWithValue("@p_status", order.Status);

            var resultParam = new MySqlParameter("@p_result", MySqlDbType.Int32);
            resultParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(resultParam);

            await cmd.ExecuteNonQueryAsync();

            int result = Convert.ToInt32(resultParam.Value);

            return result == 1;
        }
    }
}
