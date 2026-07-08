using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentCalculator.Common.Enum;
using PaymentCalculator.Data;
using PaymentCalculator.Data.Repositories;
using PaymentCalculator.Data.UnitOfWork;
using PaymentCalculator.Factories;
using PaymentCalculator.Services.Payment;
using PaymentCalculator.Services.Payment.DTO;
using PaymentCalculator.Strategies;

namespace PaymentCalculator.Tests.Services;

public class PaymentServiceTests
{
    [Fact]
    public async Task ProcessPayment_ShouldPersistPaymentAndReturnCalculatedTotals()
    {
        var serviceProvider = CreateServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var paymentService = scope.ServiceProvider.GetRequiredService<PaymentService>();
        var context = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();

        var request = new PaymentRequestDto
        {
            Amount = 100m,
            PaymentMethod = PaymentMethod.CreditCard
        };

        var result = await paymentService.ProcessPayment(request);
        var payments = await context.Payments.ToListAsync();

        Assert.Equal(105m, result.TotalAmount);
        Assert.Equal(5m, result.Fee);
        Assert.Single(payments);
        Assert.Equal(PaymentMethod.CreditCard.ToString(), result.PaymentMethod);
    }

    [Fact]
    public async Task ProcessPayment_ShouldThrowForInvalidAmount()
    {
        var serviceProvider = CreateServiceProvider();
        var paymentService = serviceProvider.GetRequiredService<PaymentService>();

        var request = new PaymentRequestDto
        {
            Amount = 0m,
            PaymentMethod = PaymentMethod.Paypal
        };

        await Assert.ThrowsAsync<ArgumentException>(() => paymentService.ProcessPayment(request));
    }

    private static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddDbContext<PaymentDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPaymentStrategyFactory, PaymentStrategyFactory>();
        services.AddScoped<IPaymentStrategy, CreditCardStrategy>();
        services.AddScoped<IPaymentStrategy, PaypalStrategy>();
        services.AddScoped<IPaymentStrategy, CryptoStrategy>();
        services.AddScoped<CreditCardStrategy>();
        services.AddScoped<PaypalStrategy>();
        services.AddScoped<CryptoStrategy>();
        services.AddScoped<PaymentService>();

        return services.BuildServiceProvider();
    }
}
