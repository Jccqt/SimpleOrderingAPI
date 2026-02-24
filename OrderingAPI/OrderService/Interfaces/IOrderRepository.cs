using OrderService.DTOs.V1.OrderDTOs;
using OrderService.Models.Order;

namespace OrderService.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Orders>> GetAllOrders();
        Task<Orders> GetOrder(int orderID);
        Task<List<OrderItemDetailsDTO>> GetAllOrderItemDetails();
        Task<OrderItemDetailsDTO> GetOrderItemDetails(int orderID);
        Task<List<OrdersWithUserInfoDTO>> GetAllOrdersWithUserInfo();
        Task<OrdersWithUserInfoDTO> GetOrderWithUserInfo(int orderID);
        Task<bool> AddOrder(AddOrderDTO order);
    }
}
