using Proto;
using Proto.DependencyInjection;
using ProtoActor.TodoList.RestApi.Actors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSingleton<IActor, >()
builder.Services.AddSingleton(provider =>
{
    var actorSystemConfig = ActorSystemConfig
        .Setup();
    // Props props = Props.FromFunc(_ => new TodoServiceActor());
    var actorSystem = new ActorSystem(actorSystemConfig)
        .WithServiceProvider(provider);
    
    // actorSystem.Root.SpawnNamed(
    //     
    // )
    return actorSystem;
});

builder.Services.AddTransient<TodoServiceActor>();

// Props.FromProducer(() => )

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();