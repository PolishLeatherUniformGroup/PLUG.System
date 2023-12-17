using AutoMapper;
using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.QueryHandlers;

public sealed class GetEventEnrollmentQueryHandler : ApplicationQueryHandlerBase<GetEventEnrollmentQuery, EnrollmentResponse>
{
    private readonly IReadOnlyRepository<EventEnrollment> _eventEnrollmentRepository;
    private readonly IMapper _mapper;
    
    public GetEventEnrollmentQueryHandler(IReadOnlyRepository<EventEnrollment> eventEnrollmentRepository, IMapper mapper)
    {
        this._eventEnrollmentRepository = eventEnrollmentRepository;
        _mapper = mapper;
    }
    public override async Task<EnrollmentResponse> Handle(GetEventEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var enrollment = await this._eventEnrollmentRepository.ReadSingleById(request.EnrollmentId, cancellationToken);
        return this._mapper.Map<EnrollmentResponse>(enrollment);
    }
}