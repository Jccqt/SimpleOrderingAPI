using OrderService.DTOs.PaymentDTOs;
using OrderService.Models;

namespace OrderService.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payments>> GetAllPayments();
        Task<bool> AddPayment(AddPaymentDTO payment);
    }
}
