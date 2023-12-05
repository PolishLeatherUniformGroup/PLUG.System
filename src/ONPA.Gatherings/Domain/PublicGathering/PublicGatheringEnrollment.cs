using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Domain;

public class PublicGatheringEnrollment : Entity
{
    public DateTime RegistrationDate { get; private set; }
    public int PlacesBooked { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    public Money RequiredPayment { get; private set; }
    public Money? PaidAmount { get; private set; }
    public DateTime? PaidDate { get; private set; }

    public DateTime? CancellationDate { get; set; }
    public Money? RefundableAmount { get; private set; }
    public Money? RefundedAmount { get; private set; }
    public DateTime? RefundDate { get; private set; }

    public bool IsRefunded => this.RefundedAmount == this.RefundableAmount && this.RefundDate.HasValue;

    private List<Participant> _participants = new List<Participant>();
    public IEnumerable<Participant> Participants => this._participants;

    internal PublicGatheringEnrollment(DateTime registrationDate, int placesBooked, string firstName, string lastName,
        string email, Money price, IEnumerable<Participant> participants)
    {
        this.RegistrationDate = registrationDate;
        this.PlacesBooked = placesBooked;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.RequiredPayment = price * placesBooked;
        this._participants.Add(new Participant(firstName, lastName, email));
        if (placesBooked > 1)
        {
            this._participants.AddRange(participants);
        }
    }

    public void RegisterPayment(DateTime paidDate, Money paidAmount)
    {
        if (this.PaidAmount != null)
        {
            throw new InvalidDomainOperationException();
        }

        this.PaidDate = paidDate;
        this.PaidAmount = paidAmount;
    }

    public void Cancel(DateTime cancellationDate, Money refundableAmount)
    {
        if (this.CancellationDate != null)
        {
            throw new InvalidDomainOperationException();
        }

        this.CancellationDate = cancellationDate;
        this.RefundableAmount = refundableAmount;
    }

    public void Refund(DateTime refundDate, Money refundAmount)
    {
        if (this.RefundDate != null)
        {
            throw new InvalidDomainOperationException();
        }

        this.RefundDate = refundDate;
        this.RefundedAmount = refundAmount;
    }
}