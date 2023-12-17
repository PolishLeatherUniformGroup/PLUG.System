namespace ONPA.Membership.Contract.Responses;

public record MemberFeeResult(
    Guid MemberFeeId,
    Guid MemberId,
    decimal FeeDue,
    string Currency,
    DateTime DueDate,
    DateTime? PaidDate,
    decimal? AmountPaid,
    DateTime ValidToDate);
