using OrderingAPI.DTOs.OrderItemDTOs;
using OrderingAPI.Models;

namespace OrderingAPI.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItems>> GetAllOrderItems();
        Task<List<OrderItems>> GetOrderItems(int id);
        Task<bool> AddOrderItem(AddOrderItemDTO orderItem);
    }
}
