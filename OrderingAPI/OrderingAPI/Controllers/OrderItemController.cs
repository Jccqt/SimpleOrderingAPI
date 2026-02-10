using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.DTOs;
using OrderingAPI.DTOs.OrderItemDTOs;
using OrderingAPI.Interfaces;
using OrderingAPI.Models;
using OrderingAPI.Repositories;
using System.Data.Common;

namespace OrderingAPI.Controllers
{
    [Route("api/order-item")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _repository;

        public OrderItemController(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        // GET: api/order-item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItems()
        {
            try
            {
                var result = await _repository.GetAllOrderItems();

                var orderItems = result.Select(oi => OrderItemToOrderItemDTO(oi));

                return Ok(orderItems);
            }
            catch (DbException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured on database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
        }

        // GET: api/order-item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItems(int id)
        {
            try
            {
                var result = await _repository.GetOrderItems(id);

                var orderItems = result.Select(oi => OrderItemToOrderItemDTO(oi));

                return Ok(orderItems);
            }
            catch (DbException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured on database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
        }

        // POST: api/order-item
        [HttpPost]
        public async Task<IActionResult> AddOrderItem(AddOrderItemDTO orderItem)
        {
            bool success = await _repository.AddOrderItem(orderItem);

            if (!success)
            {
                return NotFound("Order ID or Product ID not found.");
            }

            return Ok("Order item added successfully!");
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
