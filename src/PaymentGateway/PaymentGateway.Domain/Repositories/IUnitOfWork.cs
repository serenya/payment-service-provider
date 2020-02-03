using System.Threading.Tasks;

namespace PaymentGateway.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
