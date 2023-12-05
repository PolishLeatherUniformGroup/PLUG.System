using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using ONPA.Common.Domain;

namespace ONPA.Membership.Domain;

public sealed class MembershipStatus : Enumeration
{
    public static MembershipStatus Active = new(1);
    public static MembershipStatus Suspended = new(2);
    public static MembershipStatus Expelled = new(3);
    public static MembershipStatus Expired = new(4);
    public static MembershipStatus Leaved = new(5);
    public static MembershipStatus Deleted = new(6);
    

    [JsonConstructor]
    public MembershipStatus(int value, [CallerMemberName]string displayName="") : base(value, displayName)
    {
    }
}