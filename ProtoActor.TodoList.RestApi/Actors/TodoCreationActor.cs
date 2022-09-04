using Proto;
using ProtoActor.TodoList.RestApi.Messages;

namespace ProtoActor.TodoList.RestApi.Actors;

public class TodoCreationActor : IActor
{
    public TodoCreationActor(ILogger<TodoCreationActor> logger)
    {
        Logger = logger;
    }

    private ILogger Logger { get; }

    public Task ReceiveAsync(IContext context)
    {
        var message = context.Message;

        if (message is not Add add) return Task.CompletedTask;

        Logger.LogInformation("[Add] todoItem={}", add.TodoItem);
        context.Respond(add.TodoItem);

        return Task.CompletedTask;
    }
}