using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Domain.Repositories
{
    public interface IMerchantRepository
    {
        Task<Merchant> GetAsync(Guid id);
    }
}
