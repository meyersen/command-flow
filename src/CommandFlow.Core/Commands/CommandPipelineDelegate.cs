namespace CommandFlow.Core.Commands;

public delegate Task<TResult> CommandPipelineDelegate<TResult>(CancellationToken cancellationToken);
