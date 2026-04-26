using MySql.Data.MySqlClient;
using OrderingAPI.Shared.Models.Responses;
using OrderService.DTOs.V1.OrderDTOs;
using OrderService.Interfaces;
using OrderService.Models.Order;
using System.Data;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ServiceResponse<object>> GetAllOrders()
        {
            var response = new ServiceResponse<object>();
            List<Orders> orders = new List<Orders>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM orders", conn);
            
            using var reader = await cmd.ExecuteReaderAsync();

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

            if(orders.Count > 0)
            {
                response.Success = true;
                response.Message = "Orders found.";
                response.Data = orders;
            }
            else
            {
                response.Message = "No orders found.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetOrder(int orderID)
        {
            var response = new ServiceResponse<object>();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM orders WHERE order_id = @orderID", conn);
            cmd.Parameters.AddWithValue("@orderID", orderID);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                var order =  new Orders
                {
                    order_id = Convert.ToInt32(reader["order_id"]),
                    user_id = Convert.ToInt32(reader["user_id"]),
                    order_date = Convert.ToDateTime(reader["order_date"]),
                    status = reader["status"].ToString()
                };

                response.Success = true;
                response.Message = "Order found.";
                response.Data = order;
            }
            else
            {
                response.Message = "No order found.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetAllOrderItemDetails()
        {
            var response = new ServiceResponse<object>();
            List<OrderItemDetailsModel> orders = new List<OrderItemDetailsModel>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM order_items_details", conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                var order = new OrderItemDetailsModel
                {
                    OrderID = Convert.ToInt32(reader["Order ID"]),
                    ProductID = Convert.ToInt32(reader["Product ID"]),
                    ProductName = reader["Product Name"].ToString(),
                    OrderQuantity = Convert.ToInt32(reader["Order Quantity"]),
                    OrderAmount = Convert.ToDecimal(reader["Order Amount"])
                };

                orders.Add(order);
            }

            if(orders.Count > 0)
            {
                response.Success = true;
                response.Message = "Order item details found.";
                response.Data = orders;
            }
            else
            {
                response.Message = "No order item detail found.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetOrderItemDetails(int orderID)
        {
            var response = new ServiceResponse<object>();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM order_items_details WHERE `Order ID` = @orderID", conn);
            cmd.Parameters.AddWithValue("@orderID", orderID);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                var orderItem = new OrderItemDetailsModel
                {
                    OrderID = Convert.ToInt32(reader["Order ID"]),
                    ProductID = Convert.ToInt32(reader["Product ID"]),
                    ProductName = reader["Product Name"].ToString(),
                    OrderQuantity = Convert.ToInt32(reader["Order Quantity"]),
                    OrderAmount = Convert.ToDecimal(reader["Order Amount"])
                };

                response.Success = true;
                response.Message = "Order item details found.";
                response.Data = orderItem;
            }
            else
            {
                response.Message = "No order item detail found.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetAllOrdersWithUserInfo()
        {
            var response = new ServiceResponse<object>();
            List<OrdersWithUserInfoModel> orders = new List<OrdersWithUserInfoModel>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM orders_with_user_info", conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                var order = new OrdersWithUserInfoModel
                {
                    OrderID = Convert.ToInt32(reader["Order ID"]),
                    UserID = Convert.ToInt32(reader["User ID"]),
                    UserName = reader["User Name"].ToString(),
                    Email = reader["Email"].ToString()
                };

                orders.Add(order);
            }

            if(orders.Count > 0)
            {
                response.Success = true;
                response.Message = "Orders with user info found.";
                response.Data = orders;
            }
            else
            {
                response.Message = "No orders with user info found.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetOrderWithUserInfo(int orderID)
        {
            var response = new ServiceResponse<object>();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM orders_with_user_info WHERE `Order ID` = @orderID", conn);
            cmd.Parameters.AddWithValue("@orderID", orderID);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                var order = new OrdersWithUserInfoModel
                {
                    OrderID = Convert.ToInt32(reader["Order ID"]),
                    UserID = Convert.ToInt32(reader["User ID"]),
                    UserName = reader["User Name"].ToString(),
                    Email = reader["Email"].ToString()
                };

                response.Success = true;
                response.Message = "Order with user info found.";
                response.Data = order;
            }
            else
            {
                response.Message = "No order with user info found.";
            }

            return response;
        }

        public async Task<ServiceResponse> AddOrder(AddOrderModel order)
        {
            var response = new ServiceResponse();
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

            if(result == 1)
            {
                response.Success = true;
                response.Message = "Order added successfully.";
            }
            else
            {
                response.Message = "Failed to add order.";
            }

            return response;
        }
    }
}
