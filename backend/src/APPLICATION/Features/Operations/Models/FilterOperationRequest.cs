using APPLICATION.Features.Operations.Models.Enums;
using MediatR;

namespace APPLICATION.Features.Operations.Models
{
    public class FilterOperationRequest : IRequest<OperationResponse>
    {
        public FilterOperationRequest(TypeReport type, long value)
        {
            Type = type;
            AccountId = value;
        }

        public TypeReport Type { get; set; }
        public long AccountId { get; private set; }
    }
}