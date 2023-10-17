using APPLICATION.Features.Auth.Models;
using APPLICATION.Shared.Domain;
using APPLICATION.Shared.Domain.Bases;
using APPLICATION.Shared.Helpers;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Features.Auth.Repository
{
    public class AuthRepository : RepositoryBase, IAuthRepository
    {
        public AuthRepository(IDbConnection connection, IMemoryCache memoryCache) : base(connection, memoryCache)
        {
        }

        public async Task<Account> SelectAccountAsync(FilterAccount filter, CancellationToken cancellationToken)
        {
            var query = await _path.ReadFile("SelectAccount", TypeArchives.SQL);
            return await GetOneAsync<Account>(query, filter, cancellationToken);
        }
    }
}