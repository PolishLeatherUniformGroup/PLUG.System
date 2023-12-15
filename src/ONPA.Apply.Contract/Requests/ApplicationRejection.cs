namespace ONPA.Apply.Contract.Requests;

public record ApplicationRejection(DateTime DecisionDate, string DecisionDetail, int DaysToAppeal);