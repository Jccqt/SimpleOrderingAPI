using OrderingAPI.Shared.Models.Responses;
using OrderService.Models.Order;

namespace OrderService.Interfaces
{
    public interface IOrderRepository
    {
        Task<ServiceResponse<object>> GetAllOrders();
        Task<Orders> GetOrder(int orderID);
        Task<List<OrderItemDetailsModel>> GetAllOrderItemDetails();
        Task<OrderItemDetailsModel> GetOrderItemDetails(int orderID);
        Task<List<OrdersWithUserInfoModel>> GetAllOrdersWithUserInfo();
        Task<OrdersWithUserInfoModel> GetOrderWithUserInfo(int orderID);
        Task<bool> AddOrder(AddOrderModel order);
    }
}
