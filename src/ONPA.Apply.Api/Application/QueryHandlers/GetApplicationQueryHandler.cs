using AutoMapper;
using ONPA.Apply.Api.Application.Queries;
using ONPA.Apply.Contract.Responses;
using ONPA.Apply.Infrastructure.ReadModel;
using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.QueryHandlers;

public sealed class GetApplicationQueryHandler : ApplicationQueryHandlerBase<GetApplicationQuery, ApplicationDetails?>
{
    private readonly IReadOnlyRepository<ApplicationForm> _repository;
    private readonly IMapper _mapper;
    public GetApplicationQueryHandler(IReadOnlyRepository<ApplicationForm> repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }

    public override async Task<ApplicationDetails?> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
    {
        var applicationForm= await this._repository.ReadSingleById(request.ApplicationId, cancellationToken);
        if (applicationForm is null)
        {
            return null;
        }
        var result = this._mapper.Map<ApplicationDetails>(applicationForm);
        return result;
    }
}