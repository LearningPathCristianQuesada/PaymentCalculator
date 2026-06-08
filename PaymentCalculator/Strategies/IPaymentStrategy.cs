namespace PaymentCalculator.Strategies
{
    public interface IPaymentStrategy
    {
        decimal CalculateFee(decimal amount);
        decimal CalculateTotalAmount(decimal amount);
    }
}
