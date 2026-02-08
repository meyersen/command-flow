using CommandFlow.Core.Events;

namespace CommandFlow.Example.WebAPI.Events;

public sealed class FirstUserFetchedEventHandler
    : IEventHandler<UserFetchedEvent>
{
    private readonly ILogger _logger;

    public FirstUserFetchedEventHandler(ILogger<FirstUserFetchedEventHandler> logger)
    {
        this._logger = logger;
    }

    public Task Handle(
        UserFetchedEvent @event,
        CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Called: {Handler}", nameof(FirstUserFetchedEventHandler));

        return Task.CompletedTask;
    }
}
