using CommandFlow.Core;
using CommandFlow.Core.Commands;
using CommandFlow.Example.WebAPI.Commands;
using CommandFlow.Example.WebAPI.Steps;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommandFlow(config =>
{
    config.AddCommandPipelineStep(typeof(FirstCommandPipelineStep<,>));
    config.AddCommandPipelineStep(typeof(SecondCommandPipelineStep<,>));

    config.AddEventPipelineStep(typeof(FirstEventPipelineStep<>));
    config.AddEventPipelineStep(typeof(SecondEventPipelineStep<>));

    config.AddHandlersFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/user", async (ICommandDispatcher dispatcher, CancellationToken cancellationToken) =>
{
    var user = await dispatcher.Dispatch(
        new GetUserCommand
        {
            Id = Guid.NewGuid()
        },
        cancellationToken);

    return user;
});

app.Run();
