using System.Diagnostics.CodeAnalysis;

namespace ONPA.EventBus.RabbitMq;

[ExcludeFromCodeCoverage(Justification = "Trivial")]
public class EventBusOptions
{
    public string SubscriptionClientName { get; set; }
    public int RetryCount { get; set; } = 10;
}