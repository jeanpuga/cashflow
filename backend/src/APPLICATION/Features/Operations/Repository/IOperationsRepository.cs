using APPLICATION.Features.Operations.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Features.Operations.Repository
{
    public interface IOperationsRepository
    {
        Task InsertAsync(Operation item, CancellationToken cancellationToken);

        Task<IEnumerable<Operation>> SelectBalanceAsync(FilterOperationRequest filter, CancellationToken cancellationToken);

        Task<IEnumerable<Operation>> SelectConsolidateAsync(FilterOperationRequest filter, CancellationToken cancellationToken);

        Task<IEnumerable<Operation>> SelectTodayAsync(FilterOperationRequest filter, CancellationToken cancellationToken);
    }
}