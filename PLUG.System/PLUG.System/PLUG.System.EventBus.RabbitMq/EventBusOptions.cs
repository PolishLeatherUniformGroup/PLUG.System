﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace PLUG.System.EventBus.RabbitMq;

public class EventBusOptions
{
    public string SubscriptionClientName { get; set; }
    public int RetryCount { get; set; } = 10;
}