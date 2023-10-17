using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Shared.Domain.Interfaces
{
    public interface IRabbitmqHealthServices
    {
        Task<bool> GetRabbitMqHealthcheck(CancellationToken cancellationToken);
    }
}