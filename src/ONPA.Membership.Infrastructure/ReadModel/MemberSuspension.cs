namespace ONPA.Membership.Infrastructure.ReadModel;

public class MemberSuspension
{
    public Guid MemberId { get; set; }
    public Guid TenantId { get; set; }
    public DateTime SuspensionDate { get; set; }
    public DateTime SuspendedUntil { get; set; }
    public string Justification { get; set; }
    public DateTime? AppealDate { get; set; }
    public string? AppealJustification { get; set; }
    public DateTime? AppealDecisionDate { get; set; }
}