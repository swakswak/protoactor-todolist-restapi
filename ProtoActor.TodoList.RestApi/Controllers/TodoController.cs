using Microsoft.AspNetCore.Mvc;
using Proto;
using Proto.DependencyInjection;
using ProtoActor.TodoList.RestApi.Actors;
using ProtoActor.TodoList.RestApi.Messages;

namespace ProtoActor.TodoList.RestApi.Controllers;

[ApiController]
[Route("todos")]
public class TodoController : ControllerBase
{
    private readonly ILogger _logger;

    private readonly ActorSystem _actorSystem;
    // private readonly IRootContext _rootContext;
    
    public TodoController(ILogger<TodoController> logger, ActorSystem actorSystem)
    {
        _logger = logger;
        _actorSystem = actorSystem;
        // _rootContext = rootContext;
    }

    [HttpPost]
    public Task AddTodoItem(TodoItem todoItem)
    {
        _logger.LogInformation("[Create] todoItem={}", todoItem);

        var props = _actorSystem.DI().PropsFor<TodoServiceActor>();
        var pid = _actorSystem.Root.Spawn(props);
        _actorSystem.Root.Send(pid, new Add(todoItem));
        return Task.CompletedTask;
    }
}