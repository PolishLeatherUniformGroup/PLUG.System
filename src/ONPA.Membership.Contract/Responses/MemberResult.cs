namespace ONPA.Membership.Contract.Responses;

public record MemberResult
{
    public Guid ApplicationId { get; init; }
    public string CardNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public int Status { get; init; }
    public DateTime JoinDate { get; init; }

    public MemberResult(Guid applicationId, string cardNumber, string firstName, string lastName, string email, int status, DateTime joinDate)
    {
        this.ApplicationId = applicationId;
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Status = status;
        this.JoinDate = joinDate;
    }
}

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

public record MemberSuspensionResult
{
    public DateTime SuspensionDate { get; init; }
    public DateTime SuspendedUntil { get; init; }
    public string Justification { get; init; }
    public DateTime? AppealDate { get; init; }
    public string? AppealJustification { get; init; }
    public DateTime? AppealDecisionDate { get; init; }
    public string? AppealDecisionJustification { get; init; }
}