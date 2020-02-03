using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Domain.Repositories
{
    public interface IPaymentRepository
    {
        void Add(Payment payment);

        Task<Payment> GetAsync(Guid id);
    }
}
