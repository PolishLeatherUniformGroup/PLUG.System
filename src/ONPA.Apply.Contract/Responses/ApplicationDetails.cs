namespace ONPA.Apply.Contract.Responses;

public record ApplicationDetails :ApplicationResult
{
    public string Phone { get; init; }
    public string Address { get; init; }
    public decimal? RequiredFee { get; init; }
    public decimal? PaidFee { get; init; }
    public string Currency { get; init; }
    public DateTime PaidDate { get; init; }
    public (string,bool)[] Recommenders { get; init; }
    public ApplicationAction[] Actions { get; init; }
    
    public ApplicationDetails(Guid applicationId, string firstName, string lastName, string email, int status, DateTime applicationDate, string phone, string address, decimal? requiredFee, decimal? paidFee, string currency, DateTime paidDate, (string,bool)[] recommenders, ApplicationAction[] actions) : base(applicationId, firstName, lastName, email, status, applicationDate)
    {
        this.Phone = phone;
        this.Address = address;
        this.RequiredFee = requiredFee;
        this.PaidFee = paidFee;
        this.Currency = currency;
        this.PaidDate = paidDate;
        this.Recommenders = recommenders;
        this.Actions = actions;
    }
    
}