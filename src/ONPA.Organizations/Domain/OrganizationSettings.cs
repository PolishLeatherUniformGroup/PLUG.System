using ONPA.Common.Domain;

namespace ONPA.Organizations.Domain;

public sealed class OrganizationSettings : ValueObject
{
    public int RequiredRecommendations { get; private set; }
    public int DaysForAppeal { get; private set; }
    public int FeePaymentMonth { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.RequiredRecommendations;
        yield return this.DaysForAppeal;
        yield return this.FeePaymentMonth;
    }
    
    public OrganizationSettings(int requiredRecommendations, int daysForAppeal, int feePaymentMonth)
    {
        this.RequiredRecommendations = requiredRecommendations;
        this.DaysForAppeal = daysForAppeal;
        this.FeePaymentMonth = feePaymentMonth;
    }
    public static OrganizationSettings Default => new(2, 14, 3);
   
}