using Boost.Proto.Actor.DependencyInjection;
using Microsoft.Data.Sqlite;
using Proto;
using Proto.DependencyInjection;using Proto.Persistence.Sqlite;
using ProtoActor.TodoList.RestApi.Actors;

var builder = WebApplication.CreateBuilder(args);

// var sqliteProvider = new SqliteProvider(new SqliteConnectionStringBuilder { DataSource = "todo.db" });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(provider =>
{
    var actorSystemConfig = ActorSystemConfig.Setup();
    return new ActorSystem(actorSystemConfig).WithServiceProvider(provider);
});
builder.Services.AddSingleton(typeof(IPropsFactory<>), typeof(PropsFactory<>));

builder.Services.AddSingleton<IRootContext>(serviceProvider =>
{
    var actorSystem = serviceProvider.GetRequiredService<ActorSystem>();
    var rootContext = new RootContext (actorSystem);
    var props = actorSystem.DI().PropsFor<TodoCreationActor>();
    
    rootContext.SpawnNamed(props,"TodoService");
    
    return rootContext;
});

builder.Services.AddTransient<TodoCreationActor>();

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