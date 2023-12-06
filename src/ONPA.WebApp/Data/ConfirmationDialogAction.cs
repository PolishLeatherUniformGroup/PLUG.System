namespace ONPA.WebApp.Data
{
    public class ConfirmationDialogAction
    {
        public string ActionDescription { get; set; }
        public bool Confirmed { get; set; } = false;
    }

    public class DecisionDialogAction
    {
        public string ActionDescription { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string? Decision { get; set; }
        public bool Confirmed { get; set; } = false;
    }

    public class AppealDialogAction
    {
        public string ActionDescription { get; set; }
        public DateTime? AppealDate { get; set; }
        public string? Justification { get; set; }
        public bool Confirmed { get; set; } = false;
    }

    public class PaymentDialogAction
    {
        public string ActionDescription { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public string? Currency { get; set; }
        public bool Confirmed { get; set; } = false;

    }
}
