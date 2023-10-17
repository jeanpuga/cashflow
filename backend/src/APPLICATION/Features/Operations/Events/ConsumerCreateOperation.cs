using APPLICATION.Features.Operations.Models;
using APPLICATION.Features.Operations.Models.Extensions;
using APPLICATION.Features.Operations.Repository;
using MassTransit;
using System.Threading.Tasks;

namespace APPLICATION.Features.Operations.Broker
{
    public class ConsumerCreateOperation : IConsumer<SubmitOperationMessage>
    {
        private readonly IOperationsRepository _operationsRepository;

        public ConsumerCreateOperation(IOperationsRepository operationsRepository)
        {
            _operationsRepository = operationsRepository;
        }

        public async Task Consume(ConsumeContext<SubmitOperationMessage> context)
        {
            await _operationsRepository.InsertAsync(context.Message.Mapper(), default);
        }
    }
}