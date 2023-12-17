using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.QueryHandlers;

public sealed class GetEventParticipantsQueryHandler : CollectionQueryHandlerBase<GetEventParticipantsQuery, ParticipantResponse>
{
    private readonly IReadOnlyRepository<EventEnrollment> _repository;


    public GetEventParticipantsQueryHandler(IReadOnlyRepository<EventEnrollment> repository)
    {
        this._repository = repository;
    }


    public override async Task<CollectionResult<ParticipantResponse>> Handle(GetEventParticipantsQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await this._repository.ManyByFilter(request.AsFilter());
        var participants = enrollments.SelectMany(x=>x.Participants);
        var result = participants.Skip(request.Page*request.Limit).Take(request.Limit)
            .Select(x => new ParticipantResponse( x.FirstName, x.LastName, x.Email)).ToList();
        return CollectionResult<ParticipantResponse>.FromValue(result, participants.Count());
    }
}