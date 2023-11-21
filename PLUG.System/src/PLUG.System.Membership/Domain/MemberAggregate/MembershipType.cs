using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.Domain;

public sealed class MembershipType : Enumeration
{
    public static MembershipType Regular = new(1);
    public static MembershipType Honorary = new(2);

    public MembershipType()
    {
    }

    [JsonConstructor]
    public MembershipType(int value, [CallerMemberName]string displayName="") : base(value, displayName)
    {
    }
}
