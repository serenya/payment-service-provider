using Microsoft.EntityFrameworkCore;
using PaymentGateway.Data.Entities.Configurations;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Data
{
    public class PaymentGatewayDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        public PaymentGatewayDbContext()
        {
        }

        public PaymentGatewayDbContext(DbContextOptions<PaymentGatewayDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=example");
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model
                .ApplyConfiguration(new PaymentCardConfiguration())
                .ApplyConfiguration(new PaymentConfiguration());
        }
    }
}
