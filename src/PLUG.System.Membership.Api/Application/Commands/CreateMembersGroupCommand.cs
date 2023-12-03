using PLUG.System.Common.Application;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record CreateMembersGroupCommand : ApplicationCommandBase
{
    public string GroupName { get; init; }
    public MembersGroupType GroupType { get; init; }

    public CreateMembersGroupCommand(string groupName, MembersGroupType groupType)
    {
        this.GroupName = groupName;
        this.GroupType = groupType;
    }
}