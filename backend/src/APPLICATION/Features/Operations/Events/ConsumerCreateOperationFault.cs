using APPLICATION.Features.Operations.Models;
using APPLICATION.Features.Operations.Models.Extensions;
using APPLICATION.Features.Operations.Repository;
using MassTransit;
using Serilog;
using System.Threading.Tasks;

namespace APPLICATION.Features.Operations.Broker
{
    public class ConsumerCreateOperationFault : IConsumer<Fault<SubmitOperationMessage>>
    {
        private readonly IOperationsRepository _operationsRepository;

        public ConsumerCreateOperationFault(IOperationsRepository operationsRepository)
        {
            _operationsRepository = operationsRepository;
        }

        public async Task Consume(ConsumeContext<Fault<SubmitOperationMessage>> context)
        {
            if (context.Message.Exceptions.Length > 0)
            {
                var message = context.Message as SubmitOperationMessage;
                await _operationsRepository.InsertAsync(message.Mapper(), default);
            }
            else
            {
                Log.Error("[ConsumerCreateOperationFault]Fault ReSubimit {@Exceptions}", context.Message.Exceptions);
            }
        }
    }
}