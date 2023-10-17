using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Shared.Domain.Interfaces
{
    public interface IHealthRepository
    {
        Task<bool> SelectOne(CancellationToken cancellationToken);
    }
}