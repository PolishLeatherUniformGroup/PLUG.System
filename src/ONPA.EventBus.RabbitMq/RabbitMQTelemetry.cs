using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using OpenTelemetry.Context.Propagation;

namespace ONPA.EventBus.RabbitMq;

[ExcludeFromCodeCoverage(Justification = "Trivial")]
public class RabbitMQTelemetry
{
    public static string ActivitySourceName = "EventBusRabbitMQ";

    public ActivitySource ActivitySource { get; } = new(ActivitySourceName);
    public TextMapPropagator Propagator { get; } = Propagators.DefaultTextMapPropagator;
}