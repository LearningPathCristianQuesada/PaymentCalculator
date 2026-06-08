namespace PaymentCalculator.Strategies
{
    public class CryptoStrategy : IPaymentStrategy
    {
        private const decimal FeePercentage = 0.01m;
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
