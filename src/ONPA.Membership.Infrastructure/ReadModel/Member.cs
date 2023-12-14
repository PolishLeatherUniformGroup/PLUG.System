namespace ONPA.Membership.Infrastructure.ReadModel;

public class Member
{
    public Guid OrganizationId { get; set; }
    public Guid Id { get; set; }
    public string MemberNumber { get;  set; }
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
    public string Email { get;  set; }
    public string Phone { get;  set; }
    public string Address { get;  set; }
    public DateTime JoinDate { get;  set; }
    public DateTime? TerminationDate { get;  set; }
    public DateTime MembershipValidUntil { get;  set; }
    public string? TerminationReason { get;  set; }
    public MembershipStatus Status { get; set; }
    public MembershipType MembershipType { get; set; }
}