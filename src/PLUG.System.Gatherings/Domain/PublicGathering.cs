﻿using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Gatherings.DomainEvents;
using PLUG.System.Gatherings.StateEvents;
using PLUG.System.SharedDomain;

namespace PLUG.System.Gatherings.Domain;

public class PublicGathering : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Regulations { get; private set; }
    public DateTime ScheduledStart { get; private set; }

    public int? PlannedCapacity { get; private set; }
    public int? AvailablePlaces { get; private set; }

    public Money PricePerPerson { get; private set; }

    public DateTime PublishDate { get; private set; }
    public DateTime EnrollmentDeadline { get; private set; }

    public PublicGatheringStatus Status { get; private set; }

    public bool IsCostFree => this.PricePerPerson.IsZero();

    public bool IsAvailable => this.Status == PublicGatheringStatus.Published && (!this.PlannedCapacity.HasValue ||
        this.PlannedCapacity.Value > this.AvailablePlaces.GetValueOrDefault(0) ||
        DateTime.UtcNow > this.EnrollmentDeadline);

    private readonly ICollection<PublicGatheringEnrollment>
        _registrations = new LinkedList<PublicGatheringEnrollment>();

    public IEnumerable<PublicGatheringEnrollment> Registrations => this._registrations;

    public PublicGathering(Guid aggregateId, IEnumerable<IStateEvent> changes) : base(aggregateId, changes)
    {
    }

    public PublicGathering(string name, string description, string regulations, DateTime scheduledStart,
        int? plannedCapacity, Money pricePerPerson, DateTime publishDate, DateTime enrollmentDeadline)
    {
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
        this.ScheduledStart = scheduledStart;
        this.PlannedCapacity = plannedCapacity;
        this.PricePerPerson = pricePerPerson;
        this.PublishDate = publishDate;
        this.EnrollmentDeadline = enrollmentDeadline;
        this.Status = PublicGatheringStatus.Draft;

        // var stateEvents
        var change = new PublicGatheringCreated(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        this.RaiseChangeEvent(change);
    }

    public void ModifyDescriptions(string name, string description, string regulations)
    {
        if (this.Status == PublicGatheringStatus.Archived)
        {
            throw new AggregateInvalidStateException();
        }

        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
        var change = new PublicGatheringDescriptionModified(name, description, regulations);
        this.RaiseChangeEvent(change);
        
        if (this.Status == PublicGatheringStatus.Published)
        {
            var domainEvent = new PublicGatheringDescriptionChangedDomainEvent(name, description, regulations,
                this._registrations
                    .SelectMany(r => r.Participants
                        .Select(p => p.Email)).ToList());
            this.RaiseDomainEvent(domainEvent);
        }
    }

    public void ModifySchedule(DateTime scheduledDate, DateTime publishDate, DateTime enrollmentDate)
    {
        if (this.Status != PublicGatheringStatus.Draft && this.Status != PublicGatheringStatus.ReadyToPublish)
        {
            throw new AggregateInvalidStateException();
        }

        this.ScheduledStart = scheduledDate;
        this.PublishDate = publishDate;
        this.EnrollmentDeadline = enrollmentDate;

        var change = new PublicGatheringScheduleChanged(scheduledDate, publishDate, enrollmentDate);
        this.RaiseChangeEvent(change);
    }

    public void ModifyPrice(Money price)
    {
        if (this.Status != PublicGatheringStatus.Draft && this.Status != PublicGatheringStatus.ReadyToPublish)
        {
            throw new AggregateInvalidStateException();
        }

        this.PricePerPerson = price;

        var change = new PublicGatheringPriceChanged(price);
        this.RaiseChangeEvent(change);
    }

    public void ModifyCapacity(int? newCapacity)
    {
        if (this.Status == PublicGatheringStatus.Archived)
        {
            throw new AggregateInvalidStateException();
        }

        if (this.Status == PublicGatheringStatus.Published)
        {
            if (this.PlannedCapacity.HasValue && (!newCapacity.HasValue || this.PlannedCapacity > newCapacity))
            {
                throw new InvalidDomainOperationException();
            }
            //domainEvent
        }
        this.PlannedCapacity = newCapacity;
        var change = new PublicGatheringCapacityChanged(newCapacity);
        this.RaiseChangeEvent(change);
    }

    public void Accept()
    {
        if (this.Status != PublicGatheringStatus.Draft)
        {
            throw new AggregateInvalidStateException();
        }
        this.Status = PublicGatheringStatus.ReadyToPublish;
        var change = new PublicGatheringStatusChanged(this.Status);
        this.RaiseChangeEvent(change);
    }

    public void Archive()
    {
        if (this.Status == PublicGatheringStatus.Archived)
        {
            throw new AggregateInvalidStateException();
        }

        if (DateTime.UtcNow < this.ScheduledStart && this.Status == PublicGatheringStatus.Published)
        {
            throw new AggregateInvalidStateException();
        }
        
        this.Status = PublicGatheringStatus.Archived;
        var change = new PublicGatheringStatusChanged(this.Status);
        this.RaiseChangeEvent(change);
    }
    
    public void Publish()
    {
        if (this.Status != PublicGatheringStatus.ReadyToPublish)
        {
            throw new AggregateInvalidStateException();
        }
        this.Status = PublicGatheringStatus.Published;
        var change = new PublicGatheringStatusChanged(this.Status);
        this.RaiseChangeEvent(change);
    }

    public void Enroll(DateTime registrationDate, int bookedPlaces, string firstName, string lastName, string email,
        IEnumerable<Participant> companions)
    {
        if (!this.IsAvailable)
        {
            throw new AggregateInvalidStateException();
        }
        
        //changeEvent
        //domainEvent
    }

    public void RegisterEnrollmentPayment(Guid enrollmentId, DateTime paidDate, Money paidAmount)
    {
        if (this.IsCostFree)
        {
            return;
        }
        //changeEvent
        //domainEvent
    }

    public void CancelEnrollment(Guid enrollmentId, DateTime cancellationDate, Money refundableAmount)
    {
        //changeEvent
        //domainEvent
    }

    public void RefundEnrollment(Guid enrollmentId, DateTime refundDate, Money refundAmount)
    {
        if (this.IsCostFree)
        {
            return;
        }
        //changeEvent
        //domainEvent
    }
}