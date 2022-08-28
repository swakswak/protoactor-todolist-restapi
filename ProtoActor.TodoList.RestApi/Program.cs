using Proto;
using Proto.DependencyInjection;
using ProtoActor.TodoList.RestApi.Actors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(provider =>
{
    var actorSystemConfig = ActorSystemConfig.Setup();
    return new ActorSystem(actorSystemConfig).WithServiceProvider(provider);
});

builder.Services.AddTransient<TodoServiceActor>();

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