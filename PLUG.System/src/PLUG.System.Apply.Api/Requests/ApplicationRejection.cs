namespace PLUG.System.Apply.Api__OLD.Requests.Apply;

public record ApplicationRejection(DateTime DecisionDate, string DecisionDetail, int DaysToAppeal);