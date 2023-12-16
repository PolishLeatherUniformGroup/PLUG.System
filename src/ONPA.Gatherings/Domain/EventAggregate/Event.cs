using ONPA.Gatherings.DomainEvents;
using ONPA.Gatherings.StateEvents;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Domain
{
    public sealed partial class Event : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Regulations { get; private set; }
        public DateTime ScheduledStart { get; private set; }

        public int? PlannedCapacity { get; private set; }
        public int? AvailablePlaces =>
            this.PlannedCapacity.HasValue
                ? this.PlannedCapacity.Value -
                  this._registrations.Where(r => !r.CancellationDate.HasValue)
                      .Sum(x => x.PlacesBooked)
                : null;
        

        public Money PricePerPerson { get; private set; }

        public DateTime PublishDate { get; private set; }
        public DateTime EnrollmentDeadline { get; private set; }

        public EventStatus Status { get; private set; }

        public bool IsCostFree => this.PricePerPerson.IsZero();

        public bool IsAvailable =>
            this.Status == EventStatus.Published &&
            DateTime.UtcNow < this.EnrollmentDeadline &&
            ((this.PlannedCapacity.HasValue && this.AvailablePlaces.GetValueOrDefault(0) > 0) ||
             !this.PlannedCapacity.HasValue);
                                                          

        private readonly ICollection<EventEnrollment>
            _registrations = new LinkedList<EventEnrollment>();

        public IEnumerable<EventEnrollment> Registrations => this._registrations;

        public Event(Guid aggregateId, Guid tenantId, IEnumerable<IStateEvent> changes) : base(aggregateId, tenantId,
            changes)
        {
        }

        public Event(Guid tenantId, string name, string description, string regulations, DateTime scheduledStart,
            int? plannedCapacity, Money pricePerPerson, DateTime publishDate,
            DateTime enrollmentDeadline) : base(tenantId)
        {
            this.Name = name;
            this.Description = description;
            this.Regulations = regulations;
            this.ScheduledStart = scheduledStart;
            this.PlannedCapacity = plannedCapacity;
            this.PricePerPerson = pricePerPerson;
            this.PublishDate = publishDate;
            this.EnrollmentDeadline = enrollmentDeadline;
            this.Status = EventStatus.Draft;
            
            // var stateEvents
            var change = new EventCreated(name, description, regulations, scheduledStart, plannedCapacity,
                pricePerPerson, publishDate, enrollmentDeadline);
            this.RaiseChangeEvent(change);
        }

        public void ModifyDescriptions(string name, string description, string regulations)
        {
            if (this.Status == EventStatus.Archived)
            {
                throw new AggregateInvalidStateException();
            }

            this.Name = name;
            this.Description = description;
            this.Regulations = regulations;
            var change = new EventDescriptionModified(name, description, regulations);
            this.RaiseChangeEvent(change);


            var domainEvent = new EventDescriptionChangedDomainEvent(name, description, regulations,
                this._registrations
                    .SelectMany(r => r.Participants
                        .Select(p => p.Email)).ToList());
            this.RaiseDomainEvent(domainEvent);
        }

        public void ModifySchedule(DateTime scheduledDate, DateTime publishDate, DateTime enrollmentDate)
        {
            if (this.Status != EventStatus.Draft && this.Status != EventStatus.ReadyToPublish)
            {
                throw new AggregateInvalidStateException();
            }

            this.ScheduledStart = scheduledDate;
            this.PublishDate = publishDate;
            this.EnrollmentDeadline = enrollmentDate;

            var change = new EventScheduleChanged(scheduledDate, publishDate, enrollmentDate);
            this.RaiseChangeEvent(change);
        }

        public void ModifyPrice(Money price)
        {
            if (this.Status != EventStatus.Draft && this.Status != EventStatus.ReadyToPublish)
            {
                throw new AggregateInvalidStateException();
            }

            this.PricePerPerson = price;

            var change = new EventPriceChanged(price);
            this.RaiseChangeEvent(change);
        }

        public void ModifyCapacity(int? newCapacity)
        {
            if (this.Status == EventStatus.Archived)
            {
                throw new AggregateInvalidStateException();
            }

            if (this.Status == EventStatus.Published)
            {
                if (newCapacity.HasValue && this.PlannedCapacity > newCapacity)
                {
                    throw new InvalidDomainOperationException();
                }

                var domainEvent = new EventCapacityIncreasedDomainEvent(newCapacity);
                this.RaiseDomainEvent(domainEvent);
            }

            this.PlannedCapacity = newCapacity;
            var change = new EventCapacityChanged(newCapacity);
            this.RaiseChangeEvent(change);
        }

        public void Accept()
        {
            if (this.Status != EventStatus.Draft)
            {
                throw new AggregateInvalidStateException();
            }

            this.Status = EventStatus.ReadyToPublish;
            var change = new EventStatusChanged(this.Status);
            this.RaiseChangeEvent(change);
        }

        public void Archive()
        {
            if (this.Status == EventStatus.Archived)
            {
                throw new AggregateInvalidStateException();
            }

            if (DateTime.UtcNow < this.ScheduledStart && this.Status == EventStatus.Published)
            {
                throw new AggregateInvalidStateException();
            }

            this.Status = EventStatus.Archived;
            var change = new EventStatusChanged(this.Status);
            this.RaiseChangeEvent(change);
        }

        public void Publish()
        {
            if (this.Status != EventStatus.ReadyToPublish)
            {
                throw new AggregateInvalidStateException();
            }

            this.Status = EventStatus.Published;
            var change = new EventStatusChanged(this.Status);
            this.RaiseChangeEvent(change);
        }

        public void Cancel(DateTime cancellationDate, string cancellationReason)
        {
            if (this.Status != EventStatus.Published)
            {
                throw new AggregateInvalidStateException();
            }

            this.Status = EventStatus.Cancelled;
            var change = new EventStatusChanged(this.Status);
            this.RaiseChangeEvent(change);

            var domainEvent = new EventCancelledDomainEvent(this.Name,
                this.Registrations.SelectMany(x => x.Participants).ToList(),
                cancellationReason);
            this.RaiseDomainEvent(domainEvent);
            foreach (var enrollment in this._registrations)
            {
                enrollment.Cancel(cancellationDate, enrollment.RequiredPayment);
                var enrollmentCancelled = new EnrollmentCancelled(enrollment.Id, cancellationDate, enrollment.RequiredPayment);
                this.RaiseChangeEvent(enrollmentCancelled);
                
                var enrollmentCancelledDomainEvent = new EnrollmentCancelledDomainEvent(cancellationDate,
                    enrollment.RequiredPayment, enrollment.FirstName, enrollment.Email,
                    enrollment.Participants.Where(x => x.Email != enrollment.Email).ToList());
                this.RaiseDomainEvent(enrollmentCancelledDomainEvent);
            }
        }

        public void Enroll(DateTime registrationDate, int bookedPlaces, string firstName, string lastName, string email,
            IEnumerable<Participant> companions)
        {
            if (!this.IsAvailable)
            {
                throw new AggregateInvalidStateException();
            }
            if(this.PlannedCapacity.HasValue && this.AvailablePlaces < bookedPlaces)
            {
                throw new InvalidDomainOperationException();
            }

            var enrollment = new EventEnrollment(registrationDate, bookedPlaces, firstName, lastName, email,
                this.PricePerPerson, companions);
            this._registrations.Add(enrollment);

            var change = new EnrollmentAddedToEvent(enrollment);
            
            this.RaiseChangeEvent(change);

            var domainEvent = new EnrollmentAddedToEventDomainEvent(enrollment.RequiredPayment,
                enrollment.FirstName, enrollment.Email,
                enrollment.Participants.Where(x => x.Email != enrollment.Email).ToList(), this.ScheduledStart);
            this.RaiseDomainEvent(domainEvent);
        }

        public void RegisterEnrollmentPayment(Guid enrollmentId, DateTime paidDate, Money paidAmount)
        {
            if(this.Status != EventStatus.Published)
            {
                throw new AggregateInvalidStateException();
            }
            
            if (this.IsCostFree)
            {
                return;
            }

            var enrollment = this._registrations.FirstOrDefault(e => e.Id == enrollmentId);
            if (enrollment == null)
            {
                throw new EntityNotFoundException();
            }
            
            if(enrollment.IsCancelled)
            {
                throw new InvalidDomainOperationException();
            }

            enrollment.RegisterPayment(paidDate, paidAmount);

            var change = new EnrollmentPaymentRegistered(enrollmentId, paidDate, paidAmount);
            this.RaiseChangeEvent(change);

            var domainEvent =
                new EnrollmentPaymentRegisteredDomainEvent(paidDate, paidAmount, enrollment.FirstName,
                    enrollment.Email);
            this.RaiseDomainEvent(domainEvent);
        }

        public void CancelEnrollment(Guid enrollmentId, DateTime cancellationDate, Money refundableAmount)
        {
            var enrollment = this._registrations.FirstOrDefault(e => e.Id == enrollmentId);
            if (enrollment == null)
            {
                throw new EntityNotFoundException();
            }

            enrollment.Cancel(cancellationDate, refundableAmount);

            var change = new EnrollmentCancelled(enrollmentId, cancellationDate, refundableAmount);
            this.RaiseChangeEvent(change);

            var domainEvent = new EnrollmentCancelledDomainEvent(cancellationDate, refundableAmount,
                enrollment.FirstName, enrollment.Email,
                enrollment.Participants.Where(x => x.Email != enrollment.Email).ToList());
            this.RaiseDomainEvent(domainEvent);
        }

        public void RefundEnrollment(Guid enrollmentId, DateTime refundDate, Money refundAmount)
        {
            if (this.IsCostFree)
            {
                return;
            }

            var enrollment = this._registrations.FirstOrDefault(e => e.Id == enrollmentId);
            if (enrollment == null)
            {
                throw new EntityNotFoundException();
            }

            if (enrollment.IsRefunded)
            {
                throw new InvalidDomainOperationException();
            }

            enrollment.Refund(refundDate, refundAmount);

            var change = new EnrollmentRefunded(enrollmentId, refundDate, refundAmount);
            this.RaiseChangeEvent(change);
            var domainEvent =
                new EnrollmentRefundedDomainEvent(refundDate, refundAmount, enrollment.FirstName, enrollment.Email);
            this.RaiseDomainEvent(domainEvent);
        }
    }
}