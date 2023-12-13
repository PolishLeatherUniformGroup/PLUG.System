namespace ONPA.Apply.Contract.Responses;

public record ApplicationAction
{
    public DateTime ActionDate { get; init; }
    public string Justification { get; init; }

    public ApplicationAction(DateTime actionDate, string justification)
    {
        this.ActionDate = actionDate;
        this.Justification = justification;
    }
}