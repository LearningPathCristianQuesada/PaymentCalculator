using PaymentCalculator.Factories;
using PaymentCalculator.Services.Payment;
using PaymentCalculator.Services.Payment.Abtractions;
using PaymentCalculator.Strategies;

namespace PaymentCalculator.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentStrategyFactory, PaymentStrategyFactory>();

            services.AddScoped<IPaymentStrategy, CreditCardStrategy>();
            services.AddScoped<IPaymentStrategy, CryptoStrategy>();
            services.AddScoped<IPaymentStrategy, PaypalStrategy>();

            services.AddScoped<CreditCardStrategy>();
            services.AddScoped<CryptoStrategy>();
            services.AddScoped<PaypalStrategy>();

            return services;
        }
    }
}
