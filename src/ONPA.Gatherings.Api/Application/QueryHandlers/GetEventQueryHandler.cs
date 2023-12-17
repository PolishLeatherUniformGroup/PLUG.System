using AutoMapper;
using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.QueryHandlers;

public sealed class GetEventQueryHandler : ApplicationQueryHandlerBase<GetEventQuery, EventResponse>
{
    private readonly IReadOnlyRepository<Event> _eventRepository;
    private readonly IMapper _mapper;


    public GetEventQueryHandler(IReadOnlyRepository<Event> eventRepository, IMapper mapper)
    {
        this._eventRepository = eventRepository;
        _mapper = mapper;
    }
    public override async Task<EventResponse> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var @event = await this._eventRepository.ReadSingleById(request.EventId, cancellationToken);

        return this._mapper.Map<EventResponse>(@event);

    }
}