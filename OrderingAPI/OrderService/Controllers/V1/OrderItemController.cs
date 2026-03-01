using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Interfaces;
using OrderService.Repositories;
using System.Data.Common;
using OrderService.DTOs.V1.OrderItemDTOs;
using Asp.Versioning;
using OrderService.Models.OrderItem;
using Microsoft.AspNetCore.RateLimiting;
using OrderingAPI.Shared.Models.Responses;

namespace OrderService.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/order-item")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    [Authorize]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _repository;

        public OrderItemController(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        // GET: api/v1/order-item
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

        // GET: api/v1/order-item/5
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

        // POST: api/v1/order-item
        [HttpPost]
        public async Task<ActionResult<AddOrderItemDTO>> AddOrderItem(AddOrderItemDTO orderItem)
        {
            bool success = await _repository.AddOrderItem(AddOrderItemMapper(orderItem));

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

        private AddOrderItemModel AddOrderItemMapper(AddOrderItemDTO orderItem) =>
            new AddOrderItemModel
            {
                OrderID = orderItem.OrderID,
                ProductID = orderItem.ProductID,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };
    }
}
