namespace PLUG.System.Apply.Api.Requests;

public record ApplicationRejection(DateTime DecisionDate, string DecisionDetail, int DaysToAppeal);