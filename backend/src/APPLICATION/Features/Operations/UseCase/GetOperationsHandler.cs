using APPLICATION.Features.Operations.Models;
using APPLICATION.Features.Operations.Models.Enums;
using APPLICATION.Features.Operations.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Features.Operations.UseCase
{
    public class GetOperationsHandler : IRequestHandler<FilterOperationRequest, OperationResponse>
    {
        private readonly IOperationsRepository _operationsRepository;

        public GetOperationsHandler(IOperationsRepository operationsRepository)
        {
            _operationsRepository = operationsRepository;
        }

        public async Task<OperationResponse> Handle(FilterOperationRequest request, CancellationToken cancellationToken)
        {
            var result = request.Type switch
            {
                TypeReport.Today => await _operationsRepository.SelectTodayAsync(request, cancellationToken),
                TypeReport.Consolidate => await _operationsRepository.SelectConsolidateAsync(request, cancellationToken),
                TypeReport.Balance => await _operationsRepository.SelectBalanceAsync(request, cancellationToken),
                _ => null
            };

            return new(result);
        }
    }
}