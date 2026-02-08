namespace CommandFlow.Core.Commands;

public interface ICommandDispatcher
{
    Task<TResult> Dispatch<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken);
}
