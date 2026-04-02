using System.Diagnostics;
using ECommerceLoggingApi.DTOs;
using ECommerceLoggingApi.Models;

namespace ECommerceLoggingApi.Services
{
    public class PaymentService
    {
        private static readonly List<Payment> _payments = new();
        private static int _paymentCounter = 1;

        public async Task<(Payment payment, long elapsedMs)> ProcessPaymentAsync(PaymentRequestDto request)
        {
            var stopwatch = Stopwatch.StartNew();

            // Simulate external API call delay
            await Task.Delay(new Random().Next(1000, 7000));

            // Random failure simulation
            bool failPayment = new Random().Next(1, 5) == 3;
            if (failPayment)
            {
                throw new TimeoutException("External payment API timeout.");
            }

            stopwatch.Stop();

            var payment = new Payment
            {
                Id = _paymentCounter++,
                OrderId = request.OrderId,
                Amount = request.Amount,
                Status = "Success",
                PaidAt = DateTime.UtcNow
            };

            _payments.Add(payment);

            return (payment, stopwatch.ElapsedMilliseconds);
        }
    }
}