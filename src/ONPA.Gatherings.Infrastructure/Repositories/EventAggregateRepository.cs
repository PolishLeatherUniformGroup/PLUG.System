using Microsoft.EntityFrameworkCore;
using ONPA.Common.Infrastructure.Repositories;
using ONPA.Gatherings.Domain;
using ONPA.Gatherings.Infrastructure.Database;

namespace ONPA.Gatherings.Infrastructure.Repositories;

public class EventAggregateRepository: MultiTenantAggregateRootRepository<GatheringsContext,Event>
{

    public EventAggregateRepository(GatheringsContext context):base(context)
    {
 
    }

    protected override async Task OnAggregateCreate(Event aggregate)
    {
        await CreateModel(aggregate);
    }

    protected override async Task OnAggregateUpdate(Event aggregate)
    {
        await UpdateModel(aggregate);
    }

    private async Task CreateModel(Event aggregate)
    {
        var eventReadModel = new ReadModel.Event
        {
            Id = aggregate.AggregateId,
            TenantId = aggregate.TenantId,
            Name = aggregate.Name,
            Description = aggregate.Description,
            Regulations = aggregate.Regulations,
            ScheduledStart = aggregate.ScheduledStart,
            PlannedCapacity = aggregate.PlannedCapacity,
            PricePerPerson = aggregate.PricePerPerson.Amount,
            Currency = aggregate.PricePerPerson.Currency,
            PublishDate = aggregate.PublishDate,
            EnrollmentDeadline = aggregate.EnrollmentDeadline,
            AvailablePlaces = aggregate.AvailablePlaces,
            Status = (ReadModel.EventStatus)aggregate.Status.Value
        };
        await this._context.Events.AddAsync(eventReadModel);
    }

    private async Task UpdateModel(Event aggregate, CancellationToken cancellationToken = new())
    {
        var eventReadModel = await this._context.Events.FindAsync(new object[] { aggregate.AggregateId }, cancellationToken);
        if (eventReadModel is null)
        {
            throw new InvalidOperationException($"Event with id {aggregate.AggregateId} not found");
        }
        eventReadModel.TenantId = aggregate.TenantId;
        eventReadModel.Name = aggregate.Name;
        eventReadModel.Description = aggregate.Description;
        eventReadModel.Regulations = aggregate.Regulations;
        eventReadModel.ScheduledStart = aggregate.ScheduledStart;
        eventReadModel.PlannedCapacity = aggregate.PlannedCapacity;
        eventReadModel.PricePerPerson = aggregate.PricePerPerson.Amount;
        eventReadModel.Currency = aggregate.PricePerPerson.Currency;
        eventReadModel.PublishDate = aggregate.PublishDate;
        eventReadModel.EnrollmentDeadline = aggregate.EnrollmentDeadline;
        eventReadModel.AvailablePlaces = aggregate.AvailablePlaces;
        eventReadModel.Status = (ReadModel.EventStatus)aggregate.Status.Value;

        foreach (var enrollment in aggregate.Registrations)
        {
            var enrollmentReadModel = await this._context.EventEnrollments.FindAsync(new object[] { enrollment.Id }, cancellationToken);
            if (enrollmentReadModel is null)
            {
                enrollmentReadModel = new ReadModel.EventEnrollment
                {
                    Id = enrollment.Id,
                    EventId = aggregate.AggregateId,
                    RegistrationDate = enrollment.RegistrationDate,
                    RefundDate = enrollment.RefundDate,
                    RefundedAmount = enrollment.RefundedAmount?.Amount,
                    RefundableAmount = enrollment.RefundableAmount?.Amount,
                    Currency = enrollment.RequiredPayment.Currency,
                    PaidAmount = enrollment.RequiredPayment.Amount,
                    PaidDate = enrollment.PaidDate,
                    CancellationDate = enrollment.CancellationDate,
                    RequiredPaymentAmount = enrollment.RequiredPayment.Amount,
                    Email = enrollment.Email,
                    FirstName = enrollment.FirstName,
                    LastName = enrollment.LastName,
                    PlacesBooked = enrollment.PlacesBooked,
                    Participants = enrollment.Participants.Select(p => new ReadModel.EventParticipant
                    {
                        Email = p.Email,
                        FirstName = p.FirstName,
                        LastName = p.LastName
                    }).ToList(),
                };
                await this._context.EventEnrollments.AddAsync(enrollmentReadModel, cancellationToken);
            }
            else
            {
                enrollmentReadModel.RegistrationDate = enrollment.RegistrationDate;
                enrollmentReadModel.RefundDate = enrollment.RefundDate;
                enrollmentReadModel.RefundedAmount = enrollment.RefundedAmount?.Amount;
                enrollmentReadModel.RefundableAmount = enrollment.RefundableAmount?.Amount;
                enrollmentReadModel.Currency = enrollment.RequiredPayment.Currency;
                enrollmentReadModel.PaidAmount = enrollment.RequiredPayment.Amount;
                enrollmentReadModel.PaidDate = enrollment.PaidDate;
                enrollmentReadModel.CancellationDate = enrollment.CancellationDate;
                enrollmentReadModel.RequiredPaymentAmount = enrollment.RequiredPayment.Amount;
                enrollmentReadModel.Email = enrollment.Email;
                enrollmentReadModel.FirstName = enrollment.FirstName;
                enrollmentReadModel.LastName = enrollment.LastName;
                enrollmentReadModel.PlacesBooked = enrollment.PlacesBooked;
                enrollmentReadModel.Participants = enrollment.Participants.Select(p => new ReadModel.EventParticipant
                {
                    Email = p.Email,
                    FirstName = p.FirstName,
                    LastName = p.LastName
                }).ToList();
                this._context.Entry(enrollmentReadModel).State = EntityState.Modified;
            }
        }
    }
}