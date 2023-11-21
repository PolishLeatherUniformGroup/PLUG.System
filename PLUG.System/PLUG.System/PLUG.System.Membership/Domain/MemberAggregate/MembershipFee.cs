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

    internal MembershipFee(Money dueAmount, DateTime dueDate, DateTime feeEndDate, Money? paidAmount, DateTime? paidDate)
    {
        DueAmount = dueAmount;
        DueDate = dueDate;
        FeeEndDate = feeEndDate;
        PaidAmount = paidAmount;
        PaidDate = paidDate;
    }
}