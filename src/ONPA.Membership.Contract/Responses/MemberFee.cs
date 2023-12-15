namespace ONPA.Membership.Contract.Responses;

public record MemberFee
{
    public Guid MemberFeeId { get; init; }
    public Guid MemberId { get; init; }
    public decimal FeeDue { get; init; }
    public string Currency { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime? PaidDate { get; init; }
    public decimal? AmountPaid { get; init; }
    public DateTime ValidToDate { get; init; }
    
    public MemberFee(Guid memberFeeId, Guid memberId, decimal feeDue, string currency, DateTime dueDate, DateTime validToDate, DateTime? paidDate, decimal? amountPaid
    )
    {
        this.MemberFeeId = memberFeeId;
        this.MemberId = memberId;
        this.FeeDue = feeDue;
        this.Currency = currency;
        this.DueDate = dueDate;
        this.PaidDate = paidDate;
        this.AmountPaid = amountPaid;
        this.ValidToDate = validToDate;
    }
}