namespace ProtoActor.TodoList.RestApi.Dto;

public record TodoItemDto
{
    public long Id { get; init; }
    public bool IsComplete { get; init; }
    public string Name { get; init; } = null!;
}