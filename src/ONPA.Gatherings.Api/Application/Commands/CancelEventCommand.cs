using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record CancelEventCommand(Guid TenantId, Guid EventId, DateTime CancellationDate, string CancellationReason) : ApplicationCommandBase(TenantId);