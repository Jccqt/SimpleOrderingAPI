using OrderingAPI.DTOs.PaymentDTOs;
using OrderingAPI.Models;

namespace OrderingAPI.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payments>> GetAllPayments();
        Task<bool> AddPayment(AddPaymentDTO payment);
    }
}
