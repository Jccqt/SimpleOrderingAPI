using OrderingAPI.DTOs.OrderDTOs;
using OrderingAPI.Models;

namespace OrderingAPI.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Orders>> GetAllOrders();
        Task<Orders> GetOrder(int id);
        Task<List<OrderItemDetailsDTO>> GetAllOrderItemDetails();
        Task<OrderItemDetailsDTO> GetOrderItemDetails(int id);
        Task<List<OrdersWithUserInfoDTO>> GetAllOrdersWithUserInfo();
        Task<OrdersWithUserInfoDTO> GetOrderWithUserInfo(int id);
        Task<bool> AddOrder(AddOrderDTO order);
    }
}
