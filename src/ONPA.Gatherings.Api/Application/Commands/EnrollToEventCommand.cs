using ONPA.Common.Application;
using ONPA.Gatherings.Domain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record EnrollToEventCommand(
    Guid TenantId,
    Guid EventId,
    string FirstName,
    string LastName,
    string Email,
    List<Participant> Participants,
    DateTime EnrollDate,
    string? Operator) : MultiTenantApplicationCommandBase(TenantId,
    Operator);