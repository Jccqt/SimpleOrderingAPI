using MySql.Data.MySqlClient;
using OrderingAPI.DTOs.PaymentDTOs;
using OrderingAPI.Interfaces;
using OrderingAPI.Models;
using System.Data;

namespace OrderingAPI.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Payments>> GetAllPayments()
        {
            List<Payments> payments = new List<Payments>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM payments", conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                var payment = new Payments
                {
                    payment_id = Convert.ToInt32(reader["payment_id"]),
                    order_id = Convert.ToInt32(reader["order_id"]),
                    payment_method = reader["payment_method"].ToString(),
                    amount = Convert.ToDecimal(reader["amount"]),
                    payment_date = Convert.ToDateTime(reader["payment_date"])
                };

                payments.Add(payment);
            }

            return payments;
        }

        public async Task<bool> AddPayment(AddPaymentDTO payment)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("AddPayment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_order_id", payment.OrderID);
            cmd.Parameters.AddWithValue("@p_payment_method", payment.PaymentMethod);
            cmd.Parameters.AddWithValue("@p_amount", payment.Amount);

            var resultParam = new MySqlParameter("@p_result", MySqlDbType.Int32);
            resultParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(resultParam);

            await cmd.ExecuteNonQueryAsync();

            int result = Convert.ToInt32(resultParam.Value);

            return result == 1;
        }
    }
}
