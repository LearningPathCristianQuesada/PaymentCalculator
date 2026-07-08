using Microsoft.EntityFrameworkCore;
using PaymentCalculator.Data.Models;

namespace PaymentCalculator.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
    {
    }

    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(payment => payment.Id);
            entity.Property(payment => payment.Amount).HasPrecision(18, 2);
            entity.Property(payment => payment.Fee).HasPrecision(18, 2);
            entity.Property(payment => payment.TotalAmount).HasPrecision(18, 2);
            entity.Property(payment => payment.PaymentMethod).IsRequired().HasMaxLength(50);
            entity.Property(payment => payment.CreatedAt).IsRequired();
        });
    }
}
