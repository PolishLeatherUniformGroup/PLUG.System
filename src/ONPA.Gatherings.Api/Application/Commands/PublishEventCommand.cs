using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record PublishEventCommand(Guid TenantId, Guid EventId) : ApplicationCommandBase(TenantId);