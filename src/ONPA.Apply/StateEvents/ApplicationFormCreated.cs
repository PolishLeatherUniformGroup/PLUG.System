using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed  class ApplicationFormCreated : StateEventBase
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public List<string> Recommendations { get; private set; }
    public string Address { get; private set; }

    public DateTime ApplicationDate { get; private set; }

    public ApplicationFormCreated(Guid tenantId, string firstName, string lastName, string email, string phone,
        List<string> recommendations,
        string address, DateTime applicationDate)
    {
        this.TenantId = tenantId;
        this.FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        this.LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        this.Email = email ?? throw new ArgumentNullException(nameof(email));
        this.Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        this.Recommendations = recommendations ?? throw new ArgumentNullException(nameof(recommendations));
        this.Address = address ?? throw new ArgumentNullException(nameof(address));
        this.ApplicationDate = applicationDate;
    }

    private ApplicationFormCreated(Guid aggregateId, long aggregateVersion, Guid tenantId,string firstName, string lastName,
        string email, string phone, List<string> recommendations, string address, DateTime applicationDate) : base(aggregateId, aggregateVersion)
    {
        this.TenantId = tenantId;
        this.FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        this.LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        this.Email = email ?? throw new ArgumentNullException(nameof(email));
        this.Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        this.Recommendations = recommendations ?? throw new ArgumentNullException(nameof(recommendations));
        this.Address = address ?? throw new ArgumentNullException(nameof(address));
        this.ApplicationDate = applicationDate;
    }

    public Guid TenantId { get; private set; }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationFormCreated(aggregateId, aggregateVersion,this.TenantId, this.FirstName, this.LastName, this.Email, this.Phone, this.Recommendations, this.Address,this.ApplicationDate);
    }
}