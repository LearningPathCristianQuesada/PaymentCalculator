namespace PaymentCalculator.Strategies
{
    public class CreditCardStrategy : IPaymentStrategy
    {
        private const decimal FeePercentage = 0.05m;
        public decimal CalculateFee(decimal amount)
        {
            return amount * FeePercentage;
        }

        public decimal CalculateTotalAmount(decimal amount)
        {
            return amount + CalculateFee(amount);
        }
    }
}
