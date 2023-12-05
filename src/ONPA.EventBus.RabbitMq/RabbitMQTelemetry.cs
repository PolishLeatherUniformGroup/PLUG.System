using System.Diagnostics;
using OpenTelemetry.Context.Propagation;

namespace ONPA.EventBus.RabbitMq;

public class RabbitMQTelemetry
{
    public static string ActivitySourceName = "EventBusRabbitMQ";

    public ActivitySource ActivitySource { get; } = new(ActivitySourceName);
    public TextMapPropagator Propagator { get; } = Propagators.DefaultTextMapPropagator;
}