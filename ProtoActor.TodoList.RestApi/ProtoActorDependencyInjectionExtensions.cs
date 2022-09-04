using Proto;
using Proto.DependencyInjection;

namespace ProtoActor.TodoList.RestApi;

public static class ProtoActorDependencyInjectionExtensions
{
    public static IHostBuilder UseProtoActor(
        this IHostBuilder host,
        ProtoActorHostedServiceStart protoActorHostedServiceStart)
    {
        host.ConfigureServices((hostBuilderContext, services) =>
        {
            services.AddSingleton(protoActorHostedServiceStart);
            services.AddSingleton(serviceProvider =>
                new ActorSystem(ActorSystemConfig.Setup())
                    .WithServiceProvider(serviceProvider));
            services.AddSingleton<IRootContext>(serviceProvider =>
            {
                var actorSystem = serviceProvider.GetRequiredService<ActorSystem>();
                return new RootContext(actorSystem);
            });
            services.AddHostedService<ProtoActorHostedService>();
        });

        return host;
    }
}