using CommandFlow.Core.Commands;
using CommandFlow.Core.Events;
using CommandFlow.Example.WebAPI.Events;

namespace CommandFlow.Example.WebAPI.Commands;

public sealed class GetUserCommandHandler
    : ICommandHandler<GetUserCommand, User>
{
    private readonly ILogger _logger;
    private readonly IEventDispatcher _dispatcher;

    public GetUserCommandHandler(
        ILogger<GetUserCommandHandler> logger,
        IEventDispatcher eventDispatcher)
    {
        this._logger = logger;
        this._dispatcher = eventDispatcher;
    }

    public async Task<User> Handle(GetUserCommand command, CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Called: {Handler}", nameof(GetUserCommandHandler));

        var user = new User
        {
            Id = command.Id,
            Name = "James"
        };

        await this._dispatcher.Dispatch(
            new UserFetchedEvent
            {
                Id = user.Id
            },
            cancellationToken);

        return await Task.FromResult(user);
    }
}
