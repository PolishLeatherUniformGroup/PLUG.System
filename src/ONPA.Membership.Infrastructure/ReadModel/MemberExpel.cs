namespace ONPA.Membership.Infrastructure.ReadModel;

public class MemberExpel
{
    public Guid TenantId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime ExpelDate { get; set; }
    public string Justification { get; set; }
    public DateTime? AppealDate { get; set; }
    public string? AppealJustification { get; set; }
    public DateTime? AppealDecisionDate { get; set; }
}