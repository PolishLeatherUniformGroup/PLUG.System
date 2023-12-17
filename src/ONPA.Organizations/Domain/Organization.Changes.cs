using ONPA.Organizations.StateEvents;

namespace ONPA.Organizations.Domain;

public sealed partial class Organization
{
    public void ApplyChange(OrganizationCreated change)
    {
        this.Name = change.Name;
        this.CardPrefix = change.CardPrefix;
        this.TaxId = change.TaxId;
        this.AccountNumber = change.AccountNumber;
        this.Address = change.Address;
        this.ContactEmail = change.ContactEmail;
        this.Regon = change.Regon;
        this.Settings = change.Settings;
    }
    
    public void ApplyChange(OrganizationSettingsUpdated change)
    {
        this.Settings = change.Settings;
    }
    
    public void ApplyChange(MembershipFeeRequested change)
    {
        var fee = new MembershipFee(change.Id, change.Year, change.Amount);
        this._membershipFees.Add(fee);
    }
    
    public void ApplyChange(OrganizationDataUpdated change)
    {
        this.Name = change.Name;
        this.CardPrefix = change.CardPrefix;
        this.TaxId = change.TaxId;
        this.AccountNumber = change.AccountNumber;
        this.Address = change.Address;
        this.ContactEmail = change.ContactEmail;
        this.Regon = change.Regon;
    }
}