using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record RegisterEnrollmentPaymentCommand(Guid TenantId, Guid EventId, Guid EnrollmentId, DateTime PaidDate, Money PaidAmount) : ApplicationCommandBase(TenantId);