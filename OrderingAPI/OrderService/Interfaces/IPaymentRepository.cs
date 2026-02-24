using OrderService.Models.Payment;

namespace OrderService.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<Payments>> GetAllPayments();
        Task<bool> AddPayment(AddPaymentModel payment);
    }
}
