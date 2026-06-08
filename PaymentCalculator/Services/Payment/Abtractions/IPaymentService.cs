using PaymentCalculator.Services.Payment.DTO;

namespace PaymentCalculator.Services.Payment.Abtractions
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request);
    }
}
