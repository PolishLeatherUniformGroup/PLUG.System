namespace ONPA.Membership.Contract.Requests;

public record MemberExpel(DateTime ExpelDate,  string Justification, int DaysToAppeal);