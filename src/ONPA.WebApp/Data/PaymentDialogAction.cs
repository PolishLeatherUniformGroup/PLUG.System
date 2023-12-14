namespace ONPA.WebApp.Data;

public class PaymentDialogAction
{
    public string ActionDescription { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal? PaidAmount { get; set; }
    public string? Currency { get; set; }

    public DateTime? Period { get; set; }

    public bool ShowPeriod { get; set; }=false;
    public bool Confirmed { get; set; } = false;

}