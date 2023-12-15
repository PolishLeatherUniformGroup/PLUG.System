namespace ONPA.Gatherings.Contract;

public static class Routes
{
    public const string SingleEvent = "{eventId}";
    public const string SingleEventCapacity = "{eventId}/capacity";
    public const string SingleEventPrice = "{eventId}/price";
    public const string SingleEventSchedule = "{eventId}/schedule";
    public const string SingleEventCancellation = "{eventId}/schedule/cancellation";
    public const string SingleEventPublication = "{eventId}/schedule/publication";
    public const string SingleEventAcceptance = "{eventId}/acceptance";
    public const string SingleEventSingleEnrollment = "{eventId}/enrollments/{enrollmentId}";
    public const string SingleEventEnrollments = "{eventId}/enrollments";
    public const string SingleEventEnrollmentPayments = "{eventId}/enrollments/{enrollmentId}/payments";
    public const string SingleEventEnrollmentPaymentRefund = "{eventId}/enrollments/{enrollmentId}/payments/refund";
}