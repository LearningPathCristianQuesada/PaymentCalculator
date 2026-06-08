using PaymentCalculator.Common.Enum;

namespace PaymentCalculator.Services.Payment.DTO
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
