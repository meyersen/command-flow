namespace CommandFlow.Core.Events;

public delegate Task EventPipelineDelegate(CancellationToken cancellationToken);
