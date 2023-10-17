using APPLICATION.Shared.Domain;
using System.Threading.Tasks;

namespace APPLICATION.Features.Auth.Service
{
    public interface ITokenService
    {
        Task<string> GenerateToken(Account account);
    }
}