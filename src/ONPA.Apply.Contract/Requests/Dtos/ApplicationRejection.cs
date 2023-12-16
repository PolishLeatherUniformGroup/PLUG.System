namespace ONPA.Apply.Contract.Requests.Dtos;

public record ApplicationRejection(DateTime DecisionDate, string DecisionDetail, int DaysToAppeal);