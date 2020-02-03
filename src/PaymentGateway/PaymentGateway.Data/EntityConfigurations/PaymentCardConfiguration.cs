using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.ValueObjects;

namespace PaymentGateway.Data.Entities.Configurations
{
    internal class PaymentCardConfiguration : IEntityTypeConfiguration<PaymentCard>
    {
        public void Configure(EntityTypeBuilder<PaymentCard> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Number)
                .HasConversion(c => c.Value, c => new CardNumber(c))
                .IsRequired();

            builder
                .Property(c => c.HolderName)
                .IsRequired();
        }
    }
}
