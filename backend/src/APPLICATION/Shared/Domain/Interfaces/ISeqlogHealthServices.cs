using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Shared.Domain.Interfaces
{
    public interface ISeqlogHealthServices
    {
        Task<bool> GetSeqlogHealthcheck(CancellationToken cancellationToken);
    }
}