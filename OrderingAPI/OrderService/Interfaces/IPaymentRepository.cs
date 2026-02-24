using OrderService.DTOs.V1.PaymentDTOs;
using OrderService.Models.Payment;

namespace OrderService.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payments>> GetAllPayments();
        Task<bool> AddPayment(AddPaymentDTO payment);
    }
}
