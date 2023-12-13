using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Queries;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Requests;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api;

[Route("api/members")]
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
    public async Task<ActionResult<PageableResult<MemberResult>>> GetMembers([FromQuery]GetMembersRequest request)
    {
        var query = this._mapper.Map<GetMembersByStatusQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result.FromQueryResult(query));
    } 
    
    [HttpGet("{memberId}")]
    public async Task<ActionResult<MemberResult>> GetMember([FromRoute]GetMemberRequest request)
    {
        var query = this._mapper.Map<GetMemberByIdQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result);
    }
    
    [HttpGet("{memberId}/fees")]
    public async Task<ActionResult<PageableResult<MemberFee>>> GetMemberFees([FromRoute]GetMemberFeesRequest request)
    {
        var query = this._mapper.Map<GetMemberFeesQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result.FromQueryResult(query));
    }
    
    [HttpPut("{memberId}/fees/{feeId}")]
    public async Task<ActionResult<Guid>> RegisterMembershipFee([FromBody]RegisterMembershipFeePaymentRequest request)
    {
        var command = this._mapper.Map<RegisterMemberFeePaymentCommand>(request);
        var result = await this._mediator.Send(command);
        return this.Ok(result);
    }
    
    [HttpPost("{memberId}/suspensions")]
    public async Task<ActionResult<Guid>> SuspendMember([FromBody] MemberSuspensionRequest request)
    {
        var command = this._mapper.Map<SuspendMemberCommand>(request);
        var result = await this._mediator.Send(command);
        return this.Ok(result);
    }
    
    [HttpGet("{memberId}/suspensions")]
    public async Task<ActionResult<Guid>> SuspensionHistory([FromBody] GetMemberSuspensionsRequest request)
    {
        var query = this._mapper.Map<GetMemberSuspensionsQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result.FromQueryResult(query));
    }
    
    [HttpPost("{memberId}/suspensions/appeal")]
    public async Task<ActionResult<Guid>> AppealSuspension([FromBody] MemberSuspensionAppealRequest request)
    {
        var command = this._mapper.Map<AppealMemberSuspensionCommand>(request);
        var result = await this._mediator.Send(command);
        return this.Ok(result);
    }
    
    [HttpPut("{memberId}/suspensions/appeal")]
    public async Task<ActionResult<Guid>> AcceptAppealSuspension([FromBody] AcceptMemberSuspensionAppealRequest request)
    {
        var command = this._mapper.Map<AcceptSuspensionAppealCommand>(request);
        var result = await this._mediator.Send(command);
        return this.Ok(result);
    }
    
    [HttpPatch("{memberId}/suspensions/appeal")]
    public async Task<ActionResult<Guid>> DismissSuspension([FromBody] RejectMemberSuspensionAppealRequest request)
    {
        var command = this._mapper.Map<DismissSuspensionAppealCommand>(request);
        var result = await this._mediator.Send(command);
        return this.Ok(result);
    }
    
    [HttpPost("{memberId}/expels")]
    public async Task<ActionResult<Guid>> ExpelsMember([FromBody] MemberExpelRequest request)
    {
        var command = this._mapper.Map<ExpelMemberCommand>(request);
        var result = await this._mediator.Send(command);
        return this.Ok(result);
    }
    
    [HttpGet("{memberId}/expels")]
    public async Task<ActionResult<Guid>> ExpelHistory([FromBody] GetMemberExpelsRequest request)
    {
        var query = this._mapper.Map<GetMemberSuspensionsQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result.FromQueryResult(query));
    }
    
    [HttpPost("{memberId}/expels/appeal")]
    public async Task<ActionResult<Guid>> AppealExpel([FromBody] MemberExpelAppealRequest request)
    {
        var command = this._mapper.Map<AppealMemberExpelCommand>(request);
        var result = await this._mediator.Send(command);
        return this.Ok(result);
    }
    
    [HttpPut("{memberId}/expels/appeal")]
    public async Task<ActionResult<Guid>> AcceptAppealExpel([FromBody] AcceptMemberExpelAppealRequest request)
    {
        var command = this._mapper.Map<AcceptExpelAppealCommand>(request);
        var result = await this._mediator.Send(command);
        return this.Ok(result);
    }
    
    [HttpPatch("{memberId}/expels/appeal")]
    public async Task<ActionResult<Guid>> DismissAppealExpel([FromBody] RejectMemberExpelAppealRequest request)
    {
        var command = this._mapper.Map<DismissSuspensionAppealCommand>(request);
        var result = await this._mediator.Send(command);
        return this.Ok(result);
    }
}