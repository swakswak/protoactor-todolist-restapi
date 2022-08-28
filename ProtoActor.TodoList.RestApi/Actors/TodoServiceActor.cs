using Proto;
using ProtoActor.TodoList.RestApi.Messages;

namespace ProtoActor.TodoList.RestApi.Actors;

public class TodoServiceActor : IActor
{
    private readonly ILogger _logger;

    public TodoServiceActor(ILogger<TodoServiceActor> logger)
    {
        _logger = logger;
    }

    public Task ReceiveAsync(IContext context)
    {
        var message = context.Message;

        if (message is Add add)
        {
            _logger.LogInformation("[Add] todoItem={}", add.TodoItem);
        }
        
        return Task.CompletedTask;
    }
}