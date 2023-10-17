using APPLICATION.Features.Operations.Models.Enums;
using MediatR;
using System;

namespace APPLICATION.Features.Operations.Models
{
    public class CreateOperationRequest : IRequest<Unit>
    {
        public CreateOperationRequest(decimal value, string describe, TypeTransaction type, long accountId)
        {
            Value = value;
            Describe = describe;
            Type = type;
            AccountId = accountId;
        }

        public Int64 AccountId { get; set; }
        public string Describe { get; set; }
        public decimal Value { get; set; }
        public TypeTransaction Type { get; set; }
    }
}