namespace ONPA.Gatherings.Contract.Requests.Dtos;

public record UpdateEventSchedule(DateTime ScheduledStart, DateTime EnrollmentDeadline, DateTime PublishDate);