using ONPA.Gatherings.Domain;
using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.StateEvents;

public sealed class EnrollmentAddedToEvent : StateEventBase
{
    public Guid EnrollmentId { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public int BookedPlaces { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public IEnumerable<Participant> Companions { get; private set; }

    public Money RequiredPayment { get; private set; }
 
    public EnrollmentAddedToEvent(Guid Id, DateTime registrationDate, int bookedPlaces, string firstName,
        string lastName, string email,
        IEnumerable<Participant> companions, Money requiredPayment)
    {
        this.EnrollmentId = Id;
        this.RegistrationDate = registrationDate;
        this.BookedPlaces = bookedPlaces;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Companions = companions;
        this.RequiredPayment = requiredPayment;
    }


    private EnrollmentAddedToEvent(Guid tenantId, Guid aggregateId, long aggregateVersion, Guid id, DateTime registrationDate, int bookedPlaces, string firstName, string lastName, string email, IEnumerable<Participant> companions, Money requiredPayment) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.EnrollmentId = id;
        this.RegistrationDate = registrationDate;
        this.BookedPlaces = bookedPlaces;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Companions = companions;
        this.RequiredPayment = requiredPayment;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new EnrollmentAddedToEvent(tenantId, aggregateId, aggregateVersion, this.EnrollmentId, this.RegistrationDate, this.BookedPlaces, this.FirstName, this.LastName, this.Email, this.Companions,this.RequiredPayment);
    }
}