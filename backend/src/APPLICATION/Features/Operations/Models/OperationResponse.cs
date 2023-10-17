using APPLICATION.Features.Operations.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace APPLICATION.Features.Operations.Models
{
    public class OperationResponse : IOperationResponse
    {
        public IEnumerable<Operation> Result { get; }

        public OperationResponse(Operation item)
        {
            Result = new List<Operation> { item };
        }

        public OperationResponse(IEnumerable<Operation> items)
        {
            Result = items.Select(e => e);
        }
    }
}