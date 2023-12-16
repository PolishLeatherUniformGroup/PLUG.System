namespace ONPA.Membership.Contract.Requests.Dtos;

public record MemberSuspension(DateTime SuspensionDate, DateTime? ReinstatementDate, string Justification, int DaysToAppeal);