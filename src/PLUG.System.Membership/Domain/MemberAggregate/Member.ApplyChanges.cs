using PLUG.System.Membership.StateEvents;
using PLUG.System.SharedDomain.Helpers;

namespace PLUG.System.Membership.Domain;

public partial class Member
{
    public void ApplyChange(MemberCreated change)
    {
        this.MemberNumber = change.CardNumber;
        this.FirstName = change.FirstName;
        this.LastName = change.LastName;
        this.Email = change.Email;
        this.Phone = change.Phone;
        this.Address = change.Address;
        this.JoinDate = change.JoinDate;
        this.Status = MembershipStatus.Active;
        this.MembershipType = MembershipType.Regular;
        this.MembershipValidUntil = change.JoinDate.ToYearEnd();
        this._membershipFees.Add(change.PaidFee);
    }

    public void ApplyChange(MemberContactDataModified change)
    {
        this.Email = change.Email;
        this.Phone = change.Phone;
        this.Address = change.Address;
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
        this.TerminationDate = change.EffectiveDate;
        this.Status = MembershipStatus.Deleted;
    }
    public void ApplyChange(ExpelAppealApproved change)
    {
        this._expel = change.Expel;
    }

    public void ApplyChange(MemberReactivated change)
    {
        this._suspension = null;
        this._expel = null;
        this.TerminationDate = null;
        this.TerminationReason = null;
        this.Status = MembershipStatus.Active;
    }
    
    public void ApplyChange(MemberLeft change)
    {
        this.TerminationDate = change.TerminationDate;
        this.TerminationReason = change.TerminationReason;
        this.Status = MembershipStatus.Leaved;
    }
    public void ApplyChange(MembershipExpired change)
    {
        this.TerminationDate = change.TerminationDate;
        this.TerminationReason = change.TerminationReason;
        this.Status = MembershipStatus.Expired;
    }
}