using OrderingAPI.Shared.Models.Responses;
using OrderService.Models.OrderItem;

namespace OrderService.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<ServiceResponse<object>> GetAllOrderItems();
        Task<List<OrderItems>> GetOrderItems(int id);
        Task<bool> AddOrderItem(AddOrderItemModel orderItem);
    }
}
