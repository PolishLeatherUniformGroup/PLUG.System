namespace ONPA.Apply.Contract.Responses;

public record ApplicationResult
{
    public Guid ApplicationId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public int Status { get; init; }
    public DateTime ApplicationDate { get; init; }

    public ApplicationResult(Guid applicationId, string firstName, string lastName, string email, int status, DateTime applicationDate)
    {
        this.ApplicationId = applicationId;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Status = status;
        this.ApplicationDate = applicationDate;
    }
}