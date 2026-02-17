using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Interfaces;
using OrderingAPI.Shared.Models;
using OrderService.Repositories;
using System.Data.Common;
using OrderService.DTOs.OrderItemDTOs;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/order-item")]
    [ApiController]
    [Authorize]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _repository;

        public OrderItemController(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        // GET: api/order-item
        [HttpGet]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<OrderItemDTO>>>> GetOrderItems()
        {
            var result = await _repository.GetAllOrderItems();

            var orderItems = result.Select(oi => OrderItemToOrderItemDTO(oi));

            var response = new ServiceResponse<IEnumerable<OrderItemDTO>>
            {
                Success = true,
                Message = "Order items retrieved successfully!",
                Data = orderItems
            };

            return Ok(response);
        }

        // GET: api/order-item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<OrderItemDTO>>>> GetOrderItems(int id)
        {
            var result = await _repository.GetOrderItems(id);

            if(result == null)
            {
                return NotFound(new ServiceResponse<IEnumerable<OrderItemDTO>>
                {
                    Success = false,
                    Message = "Order not found."
                });
            }

            var orderItems = result.Select(oi => OrderItemToOrderItemDTO(oi));

            var response = new ServiceResponse<IEnumerable<OrderItemDTO>>
            {
                Success = true,
                Message = "Order items retrieved successfully!",
                Data = orderItems
            };

            return Ok(response);
        }

        // POST: api/order-item
        [HttpPost]
        public async Task<ActionResult<AddOrderItemDTO>> AddOrderItem(AddOrderItemDTO orderItem)
        {
            bool success = await _repository.AddOrderItem(orderItem);

            if (!success)
            {
                return NotFound(new ServiceResponse<AddOrderItemDTO>
                {
                    Success = false,
                    Message = "Order ID or Product ID not found."
                });
            }

            var response = new ServiceResponse<AddOrderItemDTO>
            {
                Success = true,
                Message = "Order item added successfully!",
                Data = orderItem
            };

            return Ok(response);
        }

        private OrderItemDTO OrderItemToOrderItemDTO(OrderItems orderItems) =>
            new OrderItemDTO
            {
                OrderItemID = orderItems.order_item_id,
                OrderID = orderItems.order_id,
                ProductID = orderItems.product_id,
                Quantity = orderItems.quantity,
                Price = orderItems.price
            };
    }
}
