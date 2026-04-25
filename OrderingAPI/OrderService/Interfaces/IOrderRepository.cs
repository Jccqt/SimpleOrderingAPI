using OrderingAPI.Shared.Models.Responses;
using OrderService.Models.Order;

namespace OrderService.Interfaces
{
    public interface IOrderRepository
    {
        Task<ServiceResponse<object>> GetAllOrders();
        Task<ServiceResponse<object>> GetOrder(int orderID);
        Task<ServiceResponse<object>> GetAllOrderItemDetails();
        Task<ServiceResponse<object>> GetOrderItemDetails(int orderID);
        Task<List<OrdersWithUserInfoModel>> GetAllOrdersWithUserInfo();
        Task<OrdersWithUserInfoModel> GetOrderWithUserInfo(int orderID);
        Task<bool> AddOrder(AddOrderModel order);
    }
}
