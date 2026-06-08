using PaymentCalculator.Common.Enum;
using PaymentCalculator.Factories;
using PaymentCalculator.Services.Payment.Abtractions;
using PaymentCalculator.Services.Payment.DTO;
using PaymentCalculator.Strategies;

namespace PaymentCalculator.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentStrategyFactory _paymentStrategyFactory;

        public PaymentService(IPaymentStrategyFactory paymentStrategyFactory)
        {
            _paymentStrategyFactory = paymentStrategyFactory;
        }

        public async Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request)
        {
            try
            {
                var paymentStrategy = _paymentStrategyFactory.GetStrategy(request.PaymentMethod)!;
                var totalAmount = paymentStrategy.CalculateTotalAmount(request.Amount);

                return new PaymentResponseDto
                {
                    Id = Guid.NewGuid(),
                    Amount = request.Amount,
                    Fee = totalAmount - request.Amount,
                    TotalAmount = totalAmount,
                    PaymentMethod = request.PaymentMethod.ToString(),
                    CreatedAt = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while processing the payment.", ex);
            }
        }
    }
}
