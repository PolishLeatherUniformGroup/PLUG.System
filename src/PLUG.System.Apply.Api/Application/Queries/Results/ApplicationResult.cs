namespace PLUG.System.Apply.Api.Application.Queries.Results;

public record ApplicationResult
{
    public Guid ApplicationId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime ApplicationDate { get; init; }


    public ApplicationResult(Guid applicationId,
        string firstName,
        string lastName,
        DateTime applicationDate)
    {
        this.ApplicationId = applicationId;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.ApplicationDate = applicationDate;
    }
}