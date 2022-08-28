namespace ProtoActor.TodoList.RestApi.Messages;

public record TodoItem
{
    public TodoItem(long id, bool isComplete, string name)
    {
        Id = id;
        IsComplete = isComplete;
        Name = name;
    }

    public long Id { get; init; }
    public bool IsComplete { get; init; }
    public string Name { get; init; } = null!;
}