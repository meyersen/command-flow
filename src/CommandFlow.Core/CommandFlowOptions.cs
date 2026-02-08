using CommandFlow.Core.Commands;
using CommandFlow.Core.Events;
using System.Reflection;

namespace CommandFlow.Core;

public sealed class CommandFlowOptions
{
    internal List<Type> CommandPipelineStepTypes { get; } = [];

    internal List<Type> EventPipelineStepTypes { get; } = [];

    internal List<(Type InterfaceType, Type ImplementationType)> HandlerTypes { get; } = [];

    public void AddCommandPipelineStep(Type pipelineStep)
        => this.CommandPipelineStepTypes.Add(pipelineStep);

    public void AddEventPipelineStep(Type pipelineStep)
        => this.EventPipelineStepTypes.Add(pipelineStep);

    public void AddHandlersFromAssemblies(params Assembly[] assemblies)
    {
        var types = assemblies
            .SelectMany(a => a.GetTypes().Where(t => !t.IsAbstract && !t.IsInterface))
            .ToList();

        var commandHandlerTypes = types
            .Where(t =>
                t.GetInterfaces().Any(i =>
                    i.IsGenericType
                    && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
            .SelectMany(t =>
                t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))
                .Select(i => (InterfaceType: i, ImplementationType: t)));

        var eventHandlerTypes = types
            .Where(t =>
                t.GetInterfaces().Any(i =>
                    i.IsGenericType
                    && i.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
            .SelectMany(t =>
                t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                .Select(i => (InterfaceType: i, ImplementationType: t)));

        this.HandlerTypes.AddRange(commandHandlerTypes);
        this.HandlerTypes.AddRange(eventHandlerTypes);
    }
}
