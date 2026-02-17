using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs.OrderDTOs;
using OrderService.Interfaces;
using OrderingAPI.Shared.Models;
using OrderService.Repositories;
using System.Data.Common;
using System.Security.Claims;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        // GET: api/orders
        [HttpGet]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<OrdersDTO>>>> GetOrders()
        {
            var result = await _repository.GetAllOrders();

            var orders = result.Select(o => OrdersToOrdersDTO(o));

            var response = new ServiceResponse<IEnumerable<OrdersDTO>>
            {
                Success = true,
                Message = "Orders retrieved successfully!",
                Data = orders
            };

            return Ok(response);
        }

        // GET: api/orders/5
        [HttpGet("{orderID}")]
        public async Task<ActionResult<ServiceResponse<OrdersDTO>>> GetOrder(int orderID)
        {
            int tokenUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string tokenRole = User.FindFirstValue(ClaimTypes.Role);

            var order = await _repository.GetOrder(orderID);

            if(order.user_id != tokenUserID || tokenRole != "Admin")
            {
                return Forbid();
            }

            if (order == null)
            {
                return NotFound(new ServiceResponse<OrdersDTO>
                {
                    Success = false,
                    Message = "Order not found."
                });
            }

            var response = new ServiceResponse<OrdersDTO>
            {
                Success = true,
                Message = "Order retrieved successfully!",
                Data = OrdersToOrdersDTO(order)
            };

            return Ok(response);
        }

        // GET: api/orders/order-items-details
        [HttpGet("order-item-details")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<OrderItemDetailsDTO>>>> GetOrderItemDetails()
        {
            var orders = await _repository.GetAllOrderItemDetails();

            var response = new ServiceResponse<IEnumerable<OrderItemDetailsDTO>>
            {
                Success = true,
                Message = "Order item details retrieved successfully!",
                Data = orders
            };

            return Ok(response);
        }

        // GET: api/orders/order-items-details?orderID={}
        [HttpGet("order-item-details/{orderID}")]
        public async Task<ActionResult<ServiceResponse<OrderItemDetailsDTO>>> GetOrderItemDetails(int orderID)
        {
            var order = await _repository.GetOrderItemDetails(orderID);

            if (order == null)
            {
                return NotFound(new ServiceResponse<OrderItemDetailsDTO>
                {
                    Success = false,
                    Message = "Order item details not found."
                });
            }

            var response = new ServiceResponse<OrderItemDetailsDTO>
            {
                Success = true,
                Message = "Order item details retrieved successfully!",
                Data = order
            };

            return Ok(response);
        }

        // GET: api/orders/orders-with-user-info
        [HttpGet("order-with-user-info")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<OrdersWithUserInfoDTO>>>> GetOrderWithUserInfo()
        {
            var orders = await _repository.GetAllOrdersWithUserInfo();

            var response = new ServiceResponse<IEnumerable<OrdersWithUserInfoDTO>>
            {
                Success = true,
                Message = "Orders with user info retrieved successfully!",
                Data = orders
            };

            return Ok(response);
        }

        // GET: api/orders/orders-with-user-info?orderID={}
        [HttpGet("orders-with-user-info/{orderID}")]
        public async Task<ActionResult<ServiceResponse<OrdersWithUserInfoDTO>>> GetOrderWithUserInfo(int orderID)
        {
            int tokenUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string tokenRole = User.FindFirstValue(ClaimTypes.Role);

            var order = await _repository.GetOrderWithUserInfo(orderID);

            if(order.UserID != tokenUserID || tokenRole != "Admin")
            {
                return Forbid();
            }

            if(order == null)
            {
                return NotFound(new ServiceResponse<OrdersWithUserInfoDTO>
                {
                    Success = false,
                    Message = "Order with user info not found."
                });
            }

            var response = new ServiceResponse<OrdersWithUserInfoDTO>
            {
                Success = true,
                Message = "Order with user info retrieved successfully!",
                Data = order
            };

            return Ok(response);
        }
 
        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AddOrderDTO>>> AddOrder(AddOrderDTO order)
        {
            if (order == null)
            {
                return BadRequest(new ServiceResponse<AddOrderDTO>
                {
                    Success = false,
                    Message = "Invalid order input."
                });
            }

            bool success = await _repository.AddOrder(order);

            if (!success)
            {
                return NotFound(new ServiceResponse<AddOrderDTO>
                {
                    Success = false,
                    Message = "Cannot add order, user not found."
                });
            }

            var response = new ServiceResponse<AddOrderDTO>
            {
                Success = true,
                Message = "Order added successfully!",
                Data = order
            };

            return Ok(response);
        }

        private OrdersDTO OrdersToOrdersDTO(    Orders order) =>
            new OrdersDTO
            {
                OrderID = order.order_id,
                UserID = order.user_id,
                OrderDate = order.order_date,
                Status = order.status
            };
    }
}
