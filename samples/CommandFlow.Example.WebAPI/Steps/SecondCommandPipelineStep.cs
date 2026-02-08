using CommandFlow.Core.Commands;

namespace CommandFlow.Example.WebAPI.Steps;

public sealed class SecondCommandPipelineStep<TCommand, TResult>
    : ICommandPipelineStep<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    private readonly ILogger _logger;

    public SecondCommandPipelineStep(ILogger<SecondCommandPipelineStep<TCommand, TResult>> logger)
    {
        this._logger = logger;
    }

    public async Task<TResult> Execute(
        TCommand command,
        CommandPipelineDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Called: {Step}", nameof(SecondCommandPipelineStep<,>));
        return await next(cancellationToken);
    }
}