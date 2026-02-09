using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.DTOs.PaymentDTOs;
using OrderingAPI.Models;
using OrderingAPI.Repositories;
using System.Data.Common;

namespace OrderingAPI.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentRepository _repository;

        public PaymentController(PaymentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            try
            {
                var result = await _repository.GetAllPayments();

                var payments = result.Select(p => PaymentsToPaymentDTO(p));

                return Ok(payments);
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

        // POST: api/payments
        [HttpPost]
        public async Task<IActionResult> AddPayment(AddPaymentDTO payment)
        {
            try
            {
                bool success = await _repository.AddPayment(payment);

                if (!success)
                {
                    return NotFound("Order ID not found.");
                }

                return Ok("Payment added successfully!");
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

        private PaymentDTO PaymentsToPaymentDTO(Payments payments) =>
            new PaymentDTO
            {
                PaymentID = payments.payment_id,
                OrderID = payments.order_id,
                PaymentMethod = payments.payment_method,
                Amount = payments.amount,
                PaymentDate = payments.payment_date
            };
    }
}
