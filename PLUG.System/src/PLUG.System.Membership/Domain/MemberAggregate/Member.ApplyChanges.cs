using PLUG.System.Membership.StateEvents;
using PLUG.System.SharedDomain.Helpers;

namespace PLUG.System.Membership.Domain;

public partial class Member
{
    public void ApplyChange(MemberCreated change)
    {
        MemberNumber = change.CardNumber;
        FirstName = change.FirstName;
        LastName = change.LastName;
        Email = change.Email;
        Phone = change.Phone;
        Address = change.Address;
        JoinDate = change.JoinDate;
        Status = MembershipStatus.Active;
        MembershipType = MembershipType.Regular;
        this.MembershipValidUntil = change.JoinDate.ToYearEnd();
         this._membershipFees.Add(change.PaidFee);
    }

    public void ApplyChange(MemberContactDataModified change)
    {
        Email = change.Email;
        Phone = change.Phone;
        Address = change.Address;
    }

    public void ApplyChange(MemberFeeRequested change)
    {
        this._membershipFees.Add(change.RequestedFee);
    }

    public void ApplyChange(MemberFeePaid change)
    {
        var fee = this._membershipFees.Single(f => f.Id == change.FeeId);
        fee.AddPayment(change.PaidAmount,change.PaidDate);
        if (fee.IsBalanced)
        {
            this.MembershipValidUntil = fee.FeeEndDate;
        }
    }

    public void ApplyChange(MemberTypeChanged change)
    {
        this.MembershipType = change.MembershipType;
    }
    
    public void ApplyChange(MemberSuspended change)
    {
        this._suspension = change.Suspension;
        this.Status = MembershipStatus.Suspended;
    }
    
    public void ApplyChange(MemberSuspensionAppealReceived change)
    {
        this._suspension = change.Suspension;
    }
    
    public void ApplyChange(SuspensionAppealDismissed change)
    {
        this._suspension = change.Suspension;
    }
    
    public void ApplyChange(SuspensionAppealApproved change)
    {
        this._suspension = change.Suspension;
    }
    
    public void ApplyChange(MemberExpelled change)
    {
        this._expel = change.Expel;
        this.Status = MembershipStatus.Expelled;
    }
    
    public void ApplyChange(MemberExpelAppealReceived change)
    {
        this._expel = change.Expel;
    }
    
    public void ApplyChange(ExpelAppealDismissed change)
    {
        this._expel = change.Expel;
    }
    public void ApplyChange(ExpelAppealApproved change)
    {
        this._expel = change.Expel;
    }
}