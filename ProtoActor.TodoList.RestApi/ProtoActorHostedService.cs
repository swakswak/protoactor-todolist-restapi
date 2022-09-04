using Proto;
using static System.Threading.Tasks.Task;

namespace ProtoActor.TodoList.RestApi;

public delegate void ProtoActorHostedServiceStart(IRootContext rootContext, IServiceProvider serviceProvider);

public class ProtoActorHostedService : IHostedService
{
    public ProtoActorHostedService(
        IRootContext rootContext,
        IServiceProvider serviceProvider,
        ILogger<ProtoActorHostedService> logger,
        ProtoActorHostedServiceStart protoActorHostedServiceStart)
    {
        RootContext = rootContext;
        ServiceProvider = serviceProvider;
        Logger = logger;
        ProtoActorHostedServiceStart = protoActorHostedServiceStart;
    }

    private IRootContext RootContext { get; }
    private IServiceProvider ServiceProvider { get; }
    private ILogger Logger { get; }

    private ProtoActorHostedServiceStart ProtoActorHostedServiceStart { get; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("[StartAsync]");
        ProtoActorHostedServiceStart(RootContext, ServiceProvider);

        await Delay(300, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("[StopAsync]");
        await RootContext.System.ShutdownAsync();
    }
}