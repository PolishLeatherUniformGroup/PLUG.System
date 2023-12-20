namespace ONPA.Membership.Infrastructure.ReadModel;

public class MemberFee
{
    public Guid TenantId { get; set; }
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public decimal DueAmount { get;  set; }
    public string Currency { get; set; }
    public DateTime DueDate { get;  set; }
    public DateTime FeeEndDate { get;  set; }          
    public decimal? PaidAmount { get;  set; }
    public DateTime? PaidDate { get;  set; }
    
}