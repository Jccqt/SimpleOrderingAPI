using OrderService.DTOs.OrderItemDTOs;
using OrderService.Models;

namespace OrderService.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItems>> GetAllOrderItems();
        Task<List<OrderItems>> GetOrderItems(int id);
        Task<bool> AddOrderItem(AddOrderItemDTO orderItem);
    }
}
