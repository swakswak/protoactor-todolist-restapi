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
    private ILogger Logger { get; }

    private IRootContext RootContext { get; }

    public TodoController(ILogger<TodoController> logger, IRootContext rootContext)
    {
        Logger = logger;
        RootContext = rootContext;
    }

    [HttpPost]
    public async Task<TodoItem> AddTodoItem(TodoItem todoItem)
    {
        Logger.LogInformation("[Create] todoItem={}", todoItem);

        var response = await RootContext.RequestAsync<TodoItem>(
            new PID(RootContext.System.Address, "TodoService"),
            new Add(todoItem)
        );
        return await Task.FromResult(response);
    }
}