using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentGatewayDbContext _dbContext;

        public PaymentRepository(PaymentGatewayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Payment payment)
            => _dbContext.Add(payment);

        public Task<Payment> GetAsync(Guid id)
            => _dbContext
                .Payments
                .Include(p => p.PaymentCard)
                .FirstOrDefaultAsync(pr => pr.Id == id);
    }
}
