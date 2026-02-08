using CommandFlow.Core.Commands;
using CommandFlow.Core.Events;
using Microsoft.Extensions.DependencyInjection;

namespace CommandFlow.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandFlow(
        this IServiceCollection services,
        Action<CommandFlowOptions> configureOptions)
    {
        var options = new CommandFlowOptions();

        configureOptions(options);

        foreach (var implType in options.CommandPipelineStepTypes)
        {
            services.AddTransient(typeof(ICommandPipelineStep<,>), implType);
        }

        foreach (var implType in options.EventPipelineStepTypes)
        {
            services.AddTransient(typeof(IEventPipelineStep<>), implType);
        }

        foreach (var (interfaceType, implType) in options.HandlerTypes)
        {
            services.AddTransient(interfaceType, implType);
        }

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddSingleton<IEventDispatcher, EventDispatcher>();

        return services;
    }
}
