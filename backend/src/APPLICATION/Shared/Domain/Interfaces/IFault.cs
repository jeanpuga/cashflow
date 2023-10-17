using MassTransit;
using System;

namespace APPLICATION.Shared.Domain.Interfaces
{
    public interface IFault<T> where T : class
    {
        Guid FaultId { get; }
        Guid? FaultedMessageId { get; }
        DateTime Timestamp { get; }
        ExceptionInfo[] Exceptions { get; }
        HostInfo Host { get; }
        T Message { get; }
    }
}