namespace ONPA.Apply.Infrastructure.ReadModel;

public class ApplicationAction
{
    public Guid TenantId { get; set; }
    public Guid ApplicationId { get; set; }
    public ApplicationActionType ActionId { get; set; }
    public DateTime DecisionDate { get; set; }
    public string DecisionJustification { get; set; }
}