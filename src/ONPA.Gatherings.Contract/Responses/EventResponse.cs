namespace ONPA.Gatherings.Contract.Responses;

public sealed record EventResponse(
    Guid Id,
    string Name,
    string Description,
    string Regulations,
    DateTime ScheduledStart,
    DateTime PublishDate,
    DateTime EnrollmentDeadline,
    int? PlannedCapacity,
    int? AvailablePlaces,
    decimal PricePerPerson,
    string Currency,
    int Status);