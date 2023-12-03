using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.Domain;

public sealed class MembersGroupType: Enumeration
{
    public static MembersGroupType OrganizationBoard = new(1);
    public static MembersGroupType RevisionCommitte = new(2);
    public static MembersGroupType CustomGroup = new(3);

    public MembersGroupType()
    {
        
    }
    [JsonConstructor]
    public MembersGroupType(int value, [CallerMemberName]string displayName="") : base(value, displayName)
    {
    }
}