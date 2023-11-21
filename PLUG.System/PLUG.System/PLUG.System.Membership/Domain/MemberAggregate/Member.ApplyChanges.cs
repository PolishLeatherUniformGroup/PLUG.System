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
}