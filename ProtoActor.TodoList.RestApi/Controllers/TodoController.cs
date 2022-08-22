using Microsoft.AspNetCore.Mvc;
using ProtoActor.TodoList.RestApi.Dto;

namespace ProtoActor.TodoList.RestApi.Controllers;

[ApiController]
[Route("todos")]
public class TodoController : ControllerBase
{
    private readonly ILogger _logger;
    
    public TodoController(ILogger<TodoController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public Task<TodoItemDto> Create(TodoItemDto todoItem)
    {
        _logger.LogInformation("[Create] todoItem={}", todoItem);

        // TODO: 구현 필요
        return null;
    }
}