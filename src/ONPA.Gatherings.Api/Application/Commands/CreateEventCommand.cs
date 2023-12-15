using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record CreateEventCommand(Guid TenantId, string Name, string Description, string Regulations, DateTime ScheduledStart, int? PlannedCapacity, Money PricePerPerson, DateTime PublishDate, DateTime EnrollmentDeadline):ApplicationCommandBase(TenantId);