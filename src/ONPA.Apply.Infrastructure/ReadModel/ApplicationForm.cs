namespace ONPA.Apply.Infrastructure.ReadModel;

public class ApplicationForm
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int Status { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public decimal? RequiredFeeAmount { get; set; }
    public decimal? PaidFeeAmount { get; set; }
    public string? FeeCurrency { get; set; }
}

public class ApplicationAction
{
    public Guid ApplicationId { get; set; }
    public ApplicationActionType ActionId { get; set; }
    public DateTime DecisionDate { get; set; }
    public string DecisionJustification { get; set; }
}

public enum ApplicationActionType
{
    Approval = 1,
    Rejection =2,
    Appeal =3,
    AppealApproval =4,
    AppealRejection =5,
}