using OrderService.Models.OrderItem;

namespace OrderService.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItems>> GetAllOrderItems();
        Task<List<OrderItems>> GetOrderItems(int id);
        Task<bool> AddOrderItem(AddOrderItemModel orderItem);
    }
}
