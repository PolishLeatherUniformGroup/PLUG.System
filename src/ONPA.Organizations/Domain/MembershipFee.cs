using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Organizations.Domain;

public sealed class MembershipFee :Entity
{
    public int Year { get; private set; }
    public Money YearlyAmount { get; private set; }

    public MembershipFee(int year, Money yearlyAmount) : base(Guid.NewGuid())
    {
        this.Year = year;
        this.YearlyAmount = yearlyAmount;
    }
}