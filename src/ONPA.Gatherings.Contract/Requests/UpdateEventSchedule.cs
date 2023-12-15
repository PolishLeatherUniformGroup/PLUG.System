namespace ONPA.Gatherings.Contract.Requests;

public record UpdateEventSchedule(DateTime ScheduledStart, DateTime EnrollmentDeadline, DateTime PublishDate);