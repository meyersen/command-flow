namespace CommandFlow.Example.WebAPI;

public sealed record User
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;
}
