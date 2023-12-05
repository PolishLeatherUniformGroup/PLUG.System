using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using ONPA.Common.Domain;

namespace ONPA.Membership.Domain;

public sealed class MembershipType : Enumeration
{
    public static MembershipType Regular = new(1);
    public static MembershipType Honorary = new(2);


    [JsonConstructor]
    public MembershipType(int value, [CallerMemberName]string displayName="") : base(value, displayName)
    {
    }
}
