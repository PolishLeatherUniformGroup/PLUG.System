using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.QueryHandlers;

public sealed class GetEventsByStatusQueryHandler : CollectionQueryHandlerBase<GetEventsByStatusQuery, EventResponse>
{
    private readonly IReadOnlyRepository<Event> _eventRepository;

    public GetEventsByStatusQueryHandler(IReadOnlyRepository<Event> eventRepository)
    {
        this._eventRepository = eventRepository;
    }

    public override async Task<CollectionResult<EventResponse>> Handle(GetEventsByStatusQuery request, CancellationToken cancellationToken)
    {
        var events = await this._eventRepository.ManyByFilter(request.AsFilter(), request.Page, request.Limit, cancellationToken);

        var result = events.Select(e => new EventResponse(e.Id, e.Name, e.Description, e.Regulations, e.ScheduledStart, e.PlannedCapacity, e.AvailablePlaces, e.PricePerPerson, e.Currency, e.PublishDate, e.EnrollmentDeadline, (int)e.Status))
            .ToList();
        return CollectionResult<EventResponse>.FromValue(result, result.Count);
    }
}