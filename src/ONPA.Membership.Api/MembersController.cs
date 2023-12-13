using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Queries;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Requests;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api;

[Route("api/applications")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public MembersController(IMediator mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<PageableResult<MemberResult>>> GetApplications([FromQuery]GetMembersRequest request)
    {
        var query = this._mapper.Map<GetMembersByStatusQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result.FromQueryResult(query));
    } 
}