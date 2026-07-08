using PaymentCalculator.Data.Repositories;
using PaymentCalculator.Data.UnitOfWork;
using PaymentCalculator.Factories;
using PaymentCalculator.Services.Payment.Abtractions;
using PaymentCalculator.Services.Payment.DTO;
using PaymentEntity = PaymentCalculator.Data.Models.Payment;

namespace PaymentCalculator.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentStrategyFactory _paymentStrategyFactory;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(
            IPaymentStrategyFactory paymentStrategyFactory,
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork)
        {
            _paymentStrategyFactory = paymentStrategyFactory;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.", nameof(request.Amount));
            }

            var paymentStrategy = _paymentStrategyFactory.GetStrategy(request.PaymentMethod)
                ?? throw new NotSupportedException($"Payment method {request.PaymentMethod} is not supported.");

            var fee = paymentStrategy.CalculateFee(request.Amount);
            var totalAmount = paymentStrategy.CalculateTotalAmount(request.Amount);

            var payment = new PaymentEntity
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                Fee = fee,
                TotalAmount = totalAmount,
                PaymentMethod = request.PaymentMethod.ToString(),
                CreatedAt = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            return new PaymentResponseDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Fee = payment.Fee,
                TotalAmount = payment.TotalAmount,
                PaymentMethod = payment.PaymentMethod,
                CreatedAt = payment.CreatedAt
            };
        }
    }
}
