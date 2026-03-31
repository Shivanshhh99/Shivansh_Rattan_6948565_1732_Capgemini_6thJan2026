using ECommerceLoggingApi.DTOs;
using ECommerceLoggingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceLoggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly PaymentService _paymentService;
        private readonly OrderService _orderService;

        public PaymentController(
            ILogger<PaymentController> logger,
            PaymentService paymentService,
            OrderService orderService)
        {
            _logger = logger;
            _paymentService = paymentService;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto request)
        {
            try
            {
                _logger.LogInformation("Payment request started for OrderId: {OrderId}, Amount: {Amount}",
                    request.OrderId, request.Amount);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid payment input received for OrderId: {OrderId}", request.OrderId);
                    return BadRequest(ModelState);
                }

                var order = _orderService.GetOrderById(request.OrderId);
                if (order == null)
                {
                    _logger.LogWarning("Payment failed. Order not found: OrderId {OrderId}", request.OrderId);
                    return NotFound("Order not found");
                }

                var (payment, elapsedMs) = await _paymentService.ProcessPaymentAsync(request);

                if (elapsedMs > 5000)
                {
                    _logger.LogWarning("Payment delay detected. OrderId: {OrderId}, TimeTaken: {ElapsedMs} ms",
                        request.OrderId, elapsedMs);
                }

                _logger.LogInformation("Payment successful for OrderId: {OrderId}, PaymentId: {PaymentId}",
                    request.OrderId, payment.Id);

                return Ok(new
                {
                    Message = "Payment processed successfully",
                    Payment = payment,
                    ProcessingTimeMs = elapsedMs
                });
            }
            catch (TimeoutException ex)
            {
                _logger.LogError(ex, "Payment timeout for OrderId: {OrderId}", request.OrderId);
                return StatusCode(504, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment failed for OrderId: {OrderId}", request.OrderId);
                return StatusCode(500, new { Message = "Payment processing failed", Details = ex.Message });
            }
        }
    }
}