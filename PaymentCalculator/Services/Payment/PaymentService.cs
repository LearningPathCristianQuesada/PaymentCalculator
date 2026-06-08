using PaymentCalculator.Services.Payment.Abtractions;
using PaymentCalculator.Services.Payment.DTO;

namespace PaymentCalculator.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        public async Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request)
        {
        }
    }
}
