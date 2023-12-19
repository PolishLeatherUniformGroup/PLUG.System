using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberContactDataModified : StateEventBase
{
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }

    public MemberContactDataModified(string email, string phone, string address)
    {
        this.Email = email;
        this.Phone = phone;
        this.Address = address;
    }

    private MemberContactDataModified(Guid tenantId, Guid aggregateId, long aggregateVersion, string email, string phone, string address) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Email = email;
        this.Phone = phone;
        this.Address = address;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberContactDataModified(tenantId,aggregateId, aggregateVersion, this.Email, this.Phone, this.Address);
    }
}