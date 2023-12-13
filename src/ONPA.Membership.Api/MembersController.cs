using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Application;
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
    
    // Create a new member POST
    // Update member contact information PUT
    // Update member type PATCH
    
    // Request membership fee payment POST 
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
        return await this.SendCommandRequest<RegisterMemberFeePaymentCommand>(request);
    }
    
    [HttpPost("{memberId}/suspensions")]
    public async Task<ActionResult<Guid>> SuspendMember([FromBody] MemberSuspensionRequest request)
    {
        return await this.SendCommandRequest<SuspendMemberCommand>(request);
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
        return await this.SendCommandRequest<AppealMemberSuspensionCommand>(request);
    }
    
    [HttpPut("{memberId}/suspensions/appeal")]
    public async Task<ActionResult<Guid>> AcceptAppealSuspension([FromBody] AcceptMemberSuspensionAppealRequest request)
    {
        return await this.SendCommandRequest<AcceptSuspensionAppealCommand>(request);
    }
    
    [HttpPatch("{memberId}/suspensions/appeal")]
    public async Task<ActionResult<Guid>> DismissSuspension([FromBody] RejectMemberSuspensionAppealRequest request)
    {
        return await this.SendCommandRequest<DismissSuspensionAppealCommand>(request);
    }
    
    [HttpPost("{memberId}/expels")]
    public async Task<ActionResult<Guid>> ExpelsMember([FromBody] MemberExpelRequest request)
    {
        return await this.SendCommandRequest<ExpelMemberCommand>(request);
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
        return await this.SendCommandRequest<AppealMemberExpelCommand>(request);
    }
    
    [HttpPut("{memberId}/expels/appeal")]
    public async Task<ActionResult<Guid>> AcceptAppealExpel([FromBody] AcceptMemberExpelAppealRequest request)
    {
        return await this.SendCommandRequest<AcceptExpelAppealCommand>(request);
    }
    
    [HttpPatch("{memberId}/expels/appeal")]
    public async Task<ActionResult<Guid>> DismissAppealExpel([FromBody] RejectMemberExpelAppealRequest request)
    {
        return await this.SendCommandRequest<DismissSuspensionAppealCommand>(request);
    }
    
    public async Task<ActionResult<Guid>> RequestFeePayment([FromBody] RequestMembershipFeePaymentRequest request)
    {
        return await this.SendCommandRequest<RequestMemberFeePaymentCommand>(request);
   
    }
    
    private async Task<ActionResult<Guid>> SendCommandRequest<TCommand>(dynamic request) where TCommand:ApplicationCommandBase
    {
        var command = this._mapper.Map<TCommand>(request);
        var result = await this._mediator.Send(command);
        if (result.IsSuccess)
        {
            return this.Ok(result.AggreagteId);
        }

        if(!result.IsValid)
        {
            return this.BadRequest(result.Errors);
        }

        return this.UnprocessableEntity(result.Errors);
    }
}