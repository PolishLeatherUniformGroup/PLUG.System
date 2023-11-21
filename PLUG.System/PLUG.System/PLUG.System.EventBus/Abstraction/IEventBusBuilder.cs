using Microsoft.Extensions.DependencyInjection;

namespace PLUG.System.EventBus.Abstraction;

public interface IEventBusBuilder
{
    public IServiceCollection Services { get; }
}