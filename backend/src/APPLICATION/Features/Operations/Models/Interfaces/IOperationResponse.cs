using System.Collections.Generic;

namespace APPLICATION.Features.Operations.Models.Interfaces
{
    public interface IOperationResponse
    {
        IEnumerable<Operation> Result { get; }
    }
}