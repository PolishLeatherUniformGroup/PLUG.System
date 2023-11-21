using System.ComponentModel.DataAnnotations;

namespace PLUG.System.Apply.Infrastructure.ReadModel;

public class Recommendation
{
    [Key]
    public Guid Id { get;  set; }
    [Required]
    public Guid ApplicationId { get;  set; }
    [Required]
    public Guid RecommendingMemberId { get;  set; }
    public string RecommendingMemberNumber { get;  set; }
    public DateTime RequestDate { get; set; }
    public int Status { get; set; }
}