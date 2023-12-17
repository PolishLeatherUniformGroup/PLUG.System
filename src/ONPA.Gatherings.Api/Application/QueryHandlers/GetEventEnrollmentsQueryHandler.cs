using AutoMapper;
using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.QueryHandlers;

public sealed class GetEventEnrollmentsQueryHandler : CollectionQueryHandlerBase<GetEventEnrollmentsQuery, EnrollmentResponse>
{
    private readonly IReadOnlyRepository<EventEnrollment> _eventEnrollmentRepository;
    private readonly IMapper _mapper;

    public GetEventEnrollmentsQueryHandler(IReadOnlyRepository<EventEnrollment> eventEnrollmentRepository, IMapper mapper)
    {
        this._eventEnrollmentRepository = eventEnrollmentRepository;
        _mapper = mapper;
    }

    public override async Task<CollectionResult<EnrollmentResponse>> Handle(GetEventEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var eventEnrollments = await this._eventEnrollmentRepository.ManyByFilter(request.AsFilter(), request.Page, request.Limit, cancellationToken);

        var result = eventEnrollments.Select(e => this._mapper.Map<EnrollmentResponse>(e)).ToList();
        return CollectionResult<EnrollmentResponse>.FromValue(result, result.Count);
    }
}