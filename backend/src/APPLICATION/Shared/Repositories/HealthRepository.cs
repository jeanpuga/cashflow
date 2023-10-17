using APPLICATION.Shared.Domain.Bases;
using APPLICATION.Shared.Domain.Interfaces;
using APPLICATION.Shared.Helpers;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Shared.Repositories
{
    public class HealthRepository : RepositoryBase, IHealthRepository
    {
        public HealthRepository(IDbConnection connection, IMemoryCache memoryCache) : base(connection, memoryCache)
        {
        }

        public async Task<bool> SelectOne(CancellationToken cancellationToken)
        {
            var query = await _path.ReadFile("SelectOne", TypeArchives.SQL);
            return await GetOneAsync<bool>(query, new object(), cancellationToken);
        }
    }
}