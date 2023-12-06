namespace ONPA.WebApp.Data
{
    public class ConfirmationAction
    {
        public string ActionDescription { get; set; }
        public bool Confirmed { get; set; } = false;
    }

    public class DecisionAction
    {
        public string ActionDescription { get; set; }
        public string? Decision { get; set; }
        public bool Confirmed { get; set; } = false;
    }
}
