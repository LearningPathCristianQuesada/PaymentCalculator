using PaymentCalculator.Common.Enum;
using PaymentCalculator.Strategies;

namespace PaymentCalculator.Factories
{
    public class PaymentStrategyFactory : IPaymentStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentStrategy? GetStrategy(PaymentMethod paymentMethod)
        {
            return paymentMethod switch
            {
                PaymentMethod.CreditCard => _serviceProvider.GetRequiredService<CreditCardStrategy>(),
                PaymentMethod.Paypal => _serviceProvider.GetRequiredService<PaypalStrategy>(),
                PaymentMethod.Crypto => _serviceProvider.GetRequiredService<CryptoStrategy>(),
                _ => throw new NotSupportedException($"Payment method {paymentMethod} is not supported.")
            };
        }
    }
}
