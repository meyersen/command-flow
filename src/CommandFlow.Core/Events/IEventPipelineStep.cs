namespace CommandFlow.Core.Events;

public interface IEventPipelineStep<TEvent>
    where TEvent : IEvent
{
    Task Execute(
        TEvent @event,
        EventPipelineDelegate next,
        CancellationToken cancellationToken);
}
