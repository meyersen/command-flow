namespace CommandFlow.Core.Commands;

public interface ICommandPipelineStep<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    Task<TResult> Execute(
        TCommand command,
        CommandPipelineDelegate<TResult> next,
        CancellationToken cancellationToken);
}