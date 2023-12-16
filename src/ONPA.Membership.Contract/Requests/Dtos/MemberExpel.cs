namespace ONPA.Membership.Contract.Requests.Dtos;

public record MemberExpel(DateTime ExpelDate,  string Justification, int DaysToAppeal);