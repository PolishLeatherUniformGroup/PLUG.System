using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.Domain;

public sealed class MembershipFee : Entity
{
    public Money DueAmount { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime FeeEndDate { get; private set; }          
    public Money? PaidAmount { get; private set; }
    public DateTime? PaidDate { get; private set; }

    public bool IsBalanced => this.PaidAmount is not null && this.PaidAmount >= this.DueAmount;


    internal MembershipFee(Money dueAmount, DateTime dueDate, DateTime feeEndDate, Money? paidAmount=null, DateTime? paidDate=null) : base(Guid.NewGuid())
    {
        this.DueAmount = dueAmount;
        this.DueDate = dueDate;
        this.FeeEndDate = feeEndDate;
        this.PaidAmount = paidAmount;
        this.PaidDate = paidDate;
    }

    internal void AddPayment(Money paidAmount, DateTime paidDate)
    {
        this.PaidAmount = paidAmount;
        this.PaidDate = paidDate;
    }
    
}