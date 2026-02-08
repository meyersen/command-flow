using CommandFlow.Core.Events;

namespace CommandFlow.Example.WebAPI.Steps;

public sealed class SecondEventPipelineStep<TEvent>
    : IEventPipelineStep<TEvent>
    where TEvent : IEvent
{
    private readonly ILogger _logger;

    public SecondEventPipelineStep(ILogger<SecondEventPipelineStep<TEvent>> logger)
    {
        this._logger = logger;
    }

    public async Task Execute(
        TEvent @event,
        EventPipelineDelegate next,
        CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Called: {Step}", nameof(SecondEventPipelineStep<>));
        await next(cancellationToken);
    }
}
