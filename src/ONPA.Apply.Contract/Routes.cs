namespace ONPA.Apply.Contract;

public static class Routes
{
    public const string Base = "api/applications";
    public const string SingleApplication = "{applicationId}";
    public const string SingleApplicationPayments = "{applicationId}/payments";
    public const string SingleApplicationAppeals = "{applicationId}/appeals";
}