using AutoMapper;
using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.QueryHandlers;

public sealed class GetEventsByStatusQueryHandler : CollectionQueryHandlerBase<GetEventsByStatusQuery, EventResponse>
{
    private readonly IReadOnlyRepository<Event> _eventRepository;
    private readonly IMapper _mapper;

    public GetEventsByStatusQueryHandler(IReadOnlyRepository<Event> eventRepository, IMapper mapper)
    {
        this._eventRepository = eventRepository;
        _mapper = mapper;
    }

    public override async Task<CollectionResult<EventResponse>> Handle(GetEventsByStatusQuery request, CancellationToken cancellationToken)
    {
        var events = await this._eventRepository.ManyByFilter(request.AsFilter(), request.Page, request.Limit, cancellationToken);

        var result = events.Select(e => this._mapper.Map<EventResponse>(e)).ToList();
        return CollectionResult<EventResponse>.FromValue(result, result.Count);
    }
}