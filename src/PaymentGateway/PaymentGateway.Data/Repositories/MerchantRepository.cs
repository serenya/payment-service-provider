using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Data.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        public Task<Merchant> GetAsync(Guid id)
        {
            // TODO: implement fetching merchant information from DB
            return Task.FromResult(new Merchant
            {
                Id = id,
                Iban = "DE91100000000123456789"
            });
        }
    }
}
