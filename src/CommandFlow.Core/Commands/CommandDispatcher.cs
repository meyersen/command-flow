using Microsoft.Extensions.DependencyInjection;

namespace CommandFlow.Core.Commands;

internal sealed class CommandDispatcher
    : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
        => this._serviceProvider = serviceProvider;

    public async Task<TResult> Dispatch<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken)
    {
        var commandType = command.GetType();
        var resultType = typeof(TResult);

        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, resultType);
        var pipelineStepType = typeof(ICommandPipelineStep<,>).MakeGenericType(commandType, resultType);

        dynamic handler = this._serviceProvider.GetRequiredService(handlerType);

        var pipelineSteps = this._serviceProvider
            .GetServices(pipelineStepType)
            .Cast<dynamic>()
            .Reverse()
            .ToList();

        CommandPipelineDelegate<TResult> next = (ct) => handler.Handle((dynamic)command, ct);

        foreach (var step in pipelineSteps)
        {
            var current = next;
            next = (ct) => step.Execute((dynamic)command, current, ct);
        }

        return await next(cancellationToken);
    }
}
