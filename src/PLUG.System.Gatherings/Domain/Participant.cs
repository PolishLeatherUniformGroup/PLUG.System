using PLUG.System.Common.Domain;

namespace PLUG.System.Gatherings.Domain;

public class Participant : ValueObject
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    public Participant(string firstName, string lastName, string email)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.FirstName;
        yield return this.LastName;
        yield return this.Email;
    }
}