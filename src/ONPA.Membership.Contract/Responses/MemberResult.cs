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