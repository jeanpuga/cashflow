using APPLICATION.Features.Operations.Models.Enums;
using System;

namespace APPLICATION.Features.Operations.Models
{
    public class Operation
    {
        public Operation()
        {
        }

        public Operation(long accountId, string describe, decimal value, TypeTransaction type)
        {
            AccountId = accountId;
            Describe = describe;
            Value = value;
            Type = type;
        }

        public long? Id { get; set; }
        public long? AccountId { get; set; }
        public DateTime? DateRef { get; set; }
        public string? Describe { get; set; }
        public decimal? Value { get; set; }
        public TypeTransaction? Type { get; set; }
    }
}