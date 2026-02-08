using CommandFlow.Core.Commands;

namespace CommandFlow.Example.WebAPI.Commands;

public sealed class GetUserCommand
    : ICommand<User>
{
    public Guid Id { get; init; }
}
