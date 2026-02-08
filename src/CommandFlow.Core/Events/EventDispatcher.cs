using Microsoft.Extensions.DependencyInjection;

namespace CommandFlow.Core.Events;

internal sealed class EventDispatcher
    : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;
    }

    public async Task Dispatch(
        IEvent @event,
        CancellationToken cancellationToken)
    {
        var eventType = @event.GetType();

        var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
        var pipelineStepType = typeof(IEventPipelineStep<>).MakeGenericType(eventType);

        var handlers = this._serviceProvider
            .GetServices(handlerType)
            .Cast<dynamic>()
            .ToList();

        var pipelineSteps = this._serviceProvider
            .GetServices(pipelineStepType)
            .Cast<dynamic>()
            .Reverse()
            .ToList();

        foreach (var handler in handlers)
        {
            EventPipelineDelegate next = (ct) => handler.Handle((dynamic)@event, ct);

            foreach (var step in pipelineSteps)
            {
                var current = next;
                next = (ct) => step.Execute((dynamic)@event, current, ct);
            }

            await next(cancellationToken);
        }
    }
}
