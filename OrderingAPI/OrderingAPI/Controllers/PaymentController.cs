using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.DTOs.PaymentDTOs;
using OrderingAPI.Interfaces;
using OrderingAPI.Models;
using OrderingAPI.Repositories;
using System.Data.Common;

namespace OrderingAPI.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _repository;

        public PaymentController(IPaymentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/payments
        [HttpGet]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<PaymentDTO>>>> GetPayments()
        {
            var result = await _repository.GetAllPayments();

            var payments = result.Select(p => PaymentsToPaymentDTO(p));

            var response = new ServiceResponse<IEnumerable<PaymentDTO>>
            {
                Success = true,
                Message = "Payments retrieved successfully!",
                Data = payments
            };

            return Ok(response);
        }

        // POST: api/payments
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AddPaymentDTO>>> AddPayment(AddPaymentDTO payment)
        {
            bool success = await _repository.AddPayment(payment);

            if (!success)
            {
                return NotFound(new ServiceResponse<AddPaymentDTO>
                {
                    Success = false,
                    Message = "Order ID not found."
                });
            }

            var response = new ServiceResponse<AddPaymentDTO>
            {
                Success = true,
                Message = "Payment added successfull!",
                Data = payment
            };

            return Ok(response);
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
