using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.StateEvents;

public sealed class MemberContactDataModified : StateEventBase
{
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }

    public MemberContactDataModified(string email, string phone, string address)
    {
        Email = email;
        Phone = phone;
        Address = address;
    }

    private MemberContactDataModified(Guid aggregateId, long aggregateVersion, string email, string phone, string address) : base(aggregateId, aggregateVersion)
    {
        Email = email;
        Phone = phone;
        Address = address;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberContactDataModified(aggregateId, aggregateVersion, Email, Phone, Address);
    }
}