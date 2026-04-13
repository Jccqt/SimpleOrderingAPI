using OrderingAPI.Shared.Models.Responses;
using OrderService.Models.OrderItem;

namespace OrderService.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<ServiceResponse<object>> GetAllOrderItems();
        Task<ServiceResponse<object>> GetOrderItems(int id);
        Task<ServiceResponse> AddOrderItem(AddOrderItemModel orderItem);
    }
}
