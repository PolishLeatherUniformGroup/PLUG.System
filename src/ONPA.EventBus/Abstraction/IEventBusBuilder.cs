using Microsoft.Extensions.DependencyInjection;

namespace ONPA.EventBus.Abstraction;

public interface IEventBusBuilder
{
    public IServiceCollection Services { get; }
}