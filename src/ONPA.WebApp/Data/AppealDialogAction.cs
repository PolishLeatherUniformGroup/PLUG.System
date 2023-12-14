namespace ONPA.WebApp.Data;

public class AppealDialogAction
{
    public string ActionDescription { get; set; }
    public DateTime? AppealDate { get; set; }
    public string? Justification { get; set; }
    public bool Confirmed { get; set; } = false;
}