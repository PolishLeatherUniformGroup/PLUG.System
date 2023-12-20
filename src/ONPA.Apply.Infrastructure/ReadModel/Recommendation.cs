using System.ComponentModel.DataAnnotations;

namespace ONPA.Apply.Infrastructure.ReadModel;

public class Recommendation
{
    public Guid TenantId { get; set; }
    public Guid Id { get; set; }
    public Guid ApplicationId { get; set; }
    public Guid RecommendingMemberId { get; set; }
    public string RecommendingMemberNumber { get; set; }
    public DateTime RequestDate { get; set; }
    public int Status { get; set; }
}