namespace ONPA.WebApp.Data;

public class MemberItem
{
    public Guid Id { get; set; }
    public string CardNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime JoinDate { get; set; }
}