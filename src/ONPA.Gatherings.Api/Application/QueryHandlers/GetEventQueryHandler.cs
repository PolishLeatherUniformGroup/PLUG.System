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