using CommandFlow.Core.Events;

namespace CommandFlow.Example.WebAPI.Events;

public sealed class SecondUserFetchedEventHandler
    : IEventHandler<UserFetchedEvent>
{
    private readonly ILogger _logger;

    public SecondUserFetchedEventHandler(ILogger<SecondUserFetchedEventHandler> logger)
    {
        this._logger = logger;
    }

    public Task Handle(
        UserFetchedEvent @event,
        CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Called: {Handler}", nameof(SecondUserFetchedEventHandler));

        return Task.CompletedTask;
    }
}
