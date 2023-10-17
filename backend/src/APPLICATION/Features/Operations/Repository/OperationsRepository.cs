using APPLICATION.Features.Operations.Models;
using APPLICATION.Shared.Domain.Bases;
using APPLICATION.Shared.Helpers;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Features.Operations.Repository
{
    public class OperationsRepository : RepositoryBase, IOperationsRepository
    {
        public OperationsRepository(IDbConnection connection, IMemoryCache memoryCache) : base(connection, memoryCache)
        {
        }

        public async Task<IEnumerable<Operation>> SelectTodayAsync(FilterOperationRequest filter, CancellationToken cancellationToken)
        {
            var query = await _path.ReadFile("SelectOperationsToday", TypeArchives.SQL);
            return await GetAsync<Operation>(query, filter, cancellationToken);
        }

        public async Task<IEnumerable<Operation>> SelectConsolidateAsync(FilterOperationRequest filter, CancellationToken cancellationToken)
        {
            var query = await _path.ReadFile("SelectOperationsConsolidate", TypeArchives.SQL);
            return await GetAsync<Operation>(query, filter, cancellationToken);
        }

        public async Task<IEnumerable<Operation>> SelectBalanceAsync(FilterOperationRequest filter, CancellationToken cancellationToken)
        {
            var query = await _path.ReadFile("SelectBalanceByAccount", TypeArchives.SQL);
            return await GetAsync<Operation>(query, filter, cancellationToken);
        }

        public async Task InsertAsync(Operation item, CancellationToken cancellationToken)
        {
            var query = await _path.ReadFile("InsertOperation", TypeArchives.SQL);
            await ExecuteAsync(query, item, cancellationToken);
        }
    }
}