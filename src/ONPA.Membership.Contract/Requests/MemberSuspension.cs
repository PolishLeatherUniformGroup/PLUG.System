namespace ONPA.Membership.Contract.Requests;

public record MemberSuspension(DateTime SuspensionDate, DateTime? ReinstatementDate, string Justification, int DaysToAppeal);