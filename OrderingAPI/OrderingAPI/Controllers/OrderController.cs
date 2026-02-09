using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.DTOs.OrderDTOs;
using OrderingAPI.Models;
using OrderingAPI.Repositories;
using System.Data.Common;

namespace OrderingAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _repository;

        public OrderController(OrderRepository repository)
        {
            _repository = repository;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersDTO>>> GetOrders()
        {
            try
            {
                var result = await _repository.GetAllOrders();

                var orders = result.Select(o => OrdersToOrdersDTO(o));

                return Ok(orders);
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

        // GET: api/orders?id={}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersDTO>> GetOrder(int id)
        {
            try
            {
                var order = await _repository.GetOrder(id);

                if(order == null)
                {
                    return NotFound("Order not found.");
                }

                return Ok(OrdersToOrdersDTO(order));
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

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDTO order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Invalid order input.");
                }

                bool success = await _repository.AddOrder(order);

                if (!success)
                {
                    return NotFound("Cannot add order, user not found.");
                }

                return Ok("Order added successfully!");
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

        private OrdersDTO OrdersToOrdersDTO(Orders order) =>
            new OrdersDTO
            {
                OrderID = order.order_id,
                UserID = order.user_id,
                OrderDate = order.order_date,
                Status = order.status
            };
    }
}
