using PaymentCalculator.Common.Enum;
using PaymentCalculator.Strategies;

namespace PaymentCalculator.Factories
{
    public interface IPaymentStrategyFactory
    {
        IPaymentStrategy? GetStrategy(PaymentMethod paymentMethod);
    }
}
