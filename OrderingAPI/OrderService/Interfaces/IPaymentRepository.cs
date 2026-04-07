using OrderingAPI.Shared.Models.Responses;
using OrderService.Models.Payment;

namespace OrderService.Interfaces
{
    public interface IPaymentRepository
    {
        Task<ServiceResponse<object>> GetAllPayments();
        Task<ServiceResponse> AddPayment(AddPaymentModel payment);
    }
}
