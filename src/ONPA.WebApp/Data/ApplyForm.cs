namespace ONPA.WebApp.Data;

public class ApplyForm
{
    public Guid OrganizationId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string[] Recommendations { get; set; }

    public ApplyForm(int size, Guid organizationId)
    {
        Recommendations = new string[size];
        OrganizationId = organizationId;
    }
}