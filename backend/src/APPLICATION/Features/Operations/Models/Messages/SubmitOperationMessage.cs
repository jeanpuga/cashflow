using APPLICATION.Features.Operations.Models.Enums;
using System;

namespace APPLICATION.Features.Operations.Models
{
    public interface SubmitOperationMessage
    {
        public Int64 AccountId { get; set; }
        public string Describe { get; set; }
        public decimal Value { get; set; }
        public TypeTransaction Type { get; set; }
    }
}