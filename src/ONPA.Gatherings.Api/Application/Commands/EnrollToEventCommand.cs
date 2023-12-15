using ONPA.Common.Application;
using ONPA.Gatherings.Domain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record EnrollToEventCommand(Guid TenantId, Guid PublicGatheringId, string FirstName, string LastName, string Email, List<Participant> Participants, DateTime EnrollDate) : ApplicationCommandBase(TenantId);