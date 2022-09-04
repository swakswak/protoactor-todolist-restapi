using Proto;
using ProtoActor.TodoList.RestApi;
using ProtoActor.TodoList.RestApi.Actors;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseProtoActor((rootContext, serviceProvider) =>
{
    var props = Props.FromProducer(() => ActivatorUtilities.CreateInstance<TodoCreationActor>(serviceProvider));
    rootContext.SpawnNamed(props, nameof(ActorNames.TodoService));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();