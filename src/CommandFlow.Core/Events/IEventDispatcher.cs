namespace CommandFlow.Core.Events;

public interface IEventDispatcher
{
    Task Dispatch(
        IEvent @event,
        CancellationToken cancellationToken);
}
