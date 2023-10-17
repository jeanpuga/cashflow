using APPLICATION.Features.Operations.Models;
using APPLICATION.Features.Operations.Models.Extensions;
using MassTransit;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Features.Operations.UseCase
{
    public class AddOperationHandler : IRequestHandler<CreateOperationRequest, Unit>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public AddOperationHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(CreateOperationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _publishEndpoint.Publish<SubmitOperationMessage>(request.Mapper(), cancellationToken);
            }
            catch (Exception exception)
            {
                Log.Error("[AddOperationHandler]Handle {@exception} - {@request}", exception, request);
            }

            return Unit.Value;
        }
    }
}