namespace ONPA.Gatherings.Contract.Responses;

public class ParticipantResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public ParticipantResponse(string firstName, string lastName, string email)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }
}