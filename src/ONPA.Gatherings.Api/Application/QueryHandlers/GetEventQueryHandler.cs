using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.QueryHandlers;

public sealed class GetEventQueryHandler : ApplicationQueryHandlerBase<GetEventQuery, EventResponse>
{
    private readonly IReadOnlyRepository<Event> _eventRepository;


    public GetEventQueryHandler(IReadOnlyRepository<Event> eventRepository)
    {
        this._eventRepository = eventRepository;
    }
    public override async Task<EventResponse> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var @event = await this._eventRepository.ReadSingleById(request.EventId, cancellationToken);

        return new EventResponse(@event.Id, @event.Name, @event.Description, @event.Regulations, @event.ScheduledStart, @event.PlannedCapacity, @event.AvailablePlaces, @event.PricePerPerson, @event.Currency, @event.PublishDate, @event.EnrollmentDeadline, (int)@event.Status);

    }
}

public sealed class GetEventEnrollmentQueryHandler : ApplicationQueryHandlerBase<GetEventEnrollmentQuery, EnrollmentResponse>
{
    private readonly IReadOnlyRepository<EventEnrollment> _eventEnrollmentRepository;
    public override async Task<EnrollmentResponse> Handle(GetEventEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var enrollment = await this._eventEnrollmentRepository.ReadSingleById(request.EnrollmentId, cancellationToken);
        return new EnrollmentResponse(enrollment.Id,
            enrollment.EventId,
            enrollment.RegistrationDate,
            enrollment.PlacesBooked,
            enrollment.FirstName,
            enrollment.LastName,
            enrollment.Email,
            enrollment.Currency,
            enrollment.RequiredPaymentAmount,
            enrollment.PaidAmount,
            enrollment.PaidDate,
            enrollment.CancellationDate,
            enrollment.RefundableAmount,
            enrollment.RefundedAmount,
            enrollment.RefundDate);
    }
}