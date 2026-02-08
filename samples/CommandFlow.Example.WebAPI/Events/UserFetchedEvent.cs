using CommandFlow.Core.Events;

namespace CommandFlow.Example.WebAPI.Events;

public sealed class UserFetchedEvent
    : IEvent
{
    public Guid Id { get; init; }
}