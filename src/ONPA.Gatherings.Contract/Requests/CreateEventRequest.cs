namespace ONPA.Gatherings.Contract.Requests;

public record CreateEventRequest(string Name, string Description, string Regulations, DateTime ScheduledStart, int? PlannedCapacity, decimal PricePerPerson, string Currency, DateTime PublishDate, DateTime EnrollmentDeadline);