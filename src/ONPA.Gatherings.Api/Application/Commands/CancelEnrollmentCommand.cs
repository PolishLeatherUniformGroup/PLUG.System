using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record CancelEnrollmentCommand(Guid TenantId, Guid PublicGatheringId, Guid EnrollmentId, Money RefundableAmount, DateTime CancellationDate) : ApplicationCommandBase(TenantId);