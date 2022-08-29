using Proto;
using ProtoActor.TodoList.RestApi.Messages;

namespace ProtoActor.TodoList.RestApi.Actors;

public class TodoCreationActor : IActor
{
    private ILogger Logger { get; }

    public TodoCreationActor(ILogger<TodoCreationActor> logger)
    {
        Logger = logger;
    }

    public Task ReceiveAsync(IContext context)
    {
        var message = context.Message;

        if (message is Add add)
        {
            Logger.LogInformation("[Add] todoItem={}", add.TodoItem);
        }
        
        return Task.CompletedTask;
    }
}