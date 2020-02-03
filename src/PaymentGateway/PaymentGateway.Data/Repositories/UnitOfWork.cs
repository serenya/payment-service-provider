using System.Threading.Tasks;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentGatewayDbContext _dbContext;

        protected UnitOfWork() { }

        public UnitOfWork(PaymentGatewayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CommitAsync()
            => _dbContext.SaveChangesAsync();
    }
}
