namespace ONPA.WebApp.Data;

public class DecisionDialogAction
{
    public string ActionDescription { get; set; }
    public DateTime? DecisionDate { get; set; }
    public string? Decision { get; set; }
    public bool Confirmed { get; set; } = false;
}