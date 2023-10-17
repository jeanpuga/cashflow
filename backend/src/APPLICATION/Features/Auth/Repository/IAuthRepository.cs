using APPLICATION.Features.Auth.Models;
using APPLICATION.Shared.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Features.Auth.Repository
{
    public interface IAuthRepository
    {
        Task<Account> SelectAccountAsync(FilterAccount filter, CancellationToken cancellationToken);
    }
}