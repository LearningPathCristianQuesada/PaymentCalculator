namespace PaymentCalculator.Strategies
{
    public class PaypalStrategy : IPaymentStrategy
    {
        private const decimal FeePercentage = 0.03m;
        private const decimal FixedFee = 0.20m;
        public decimal CalculateFee(decimal amount)
        {
            return amount * FeePercentage + FixedFee;
        }

        public decimal CalculateTotalAmount(decimal amount)
        {
            return amount + CalculateFee(amount);
        }
    }
}
