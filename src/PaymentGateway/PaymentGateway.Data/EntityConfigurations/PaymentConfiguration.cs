using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.ValueObjects;

namespace PaymentGateway.Data.Entities.Configurations
{
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.MerchantId)
                .IsRequired();

            builder
                .Property(p => p.ChargeTotal)
                .HasConversion(ct => ct.Value, p => new Price(p))
                .IsRequired();

            builder
                .Property(p => p.CurrencyCode)
                .IsRequired();

            builder
                .Property(p => p.Status)
                .IsRequired();
        }
    }
}
