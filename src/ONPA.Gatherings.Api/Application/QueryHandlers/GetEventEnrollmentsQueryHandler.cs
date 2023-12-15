using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.QueryHandlers;

public sealed class GetEventEnrollmentsQueryHandler : CollectionQueryHandlerBase<GetEventEnrollmentsQuery, EnrollmentResponse>
{
    private readonly IReadOnlyRepository<EventEnrollment> _eventEnrollmentRepository;

    public GetEventEnrollmentsQueryHandler(IReadOnlyRepository<EventEnrollment> eventEnrollmentRepository)
    {
        this._eventEnrollmentRepository = eventEnrollmentRepository;
    }

    public override async Task<CollectionResult<EnrollmentResponse>> Handle(GetEventEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var eventEnrollments = await this._eventEnrollmentRepository.ManyByFilter(request.AsFilter(), request.Page, request.Limit, cancellationToken);

        var result = eventEnrollments.Select(e => new EnrollmentResponse(e.Id,
                e.EventId,
                e.RegistrationDate,
                e.PlacesBooked,
                e.FirstName,
                e.LastName,
                e.Email,
                e.Currency,
                e.RequiredPaymentAmount,
                e.PaidAmount,
                e.PaidDate,
                e.CancellationDate,
                e.RefundableAmount,
                e.RefundedAmount,
                e.RefundDate))
            .ToList();
        return CollectionResult<EnrollmentResponse>.FromValue(result, result.Count);
    }
}