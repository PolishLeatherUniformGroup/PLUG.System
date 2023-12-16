using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Application;
using ONPA.Common.Infrastructure;
using ONPA.Common.Queries;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Api.Services;
using ONPA.Membership.Contract;
using ONPA.Membership.Contract.Requests;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Controllers;

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
    private readonly IIdentityService _identityService;
    
    public MembersController(IMediator mediator, IMapper mapper, IIdentityService identityService)
    {
        this._mediator = mediator;
        this._mapper = mapper;
        this._identityService = identityService;
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateMember([FromBody]CreateMemberRequest request)
    {
        return await this.SendCommandRequest<CreateMemberCommand>(request);
    }
   
    [HttpGet]
    public async Task<ActionResult<PageableResult<MemberResult>>> GetMembers([FromQuery]GetMembersRequest request)
    {
        var query = this._mapper.Map<GetMembersByStatusQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result.FromQueryResult(query));
    } 
    
    [HttpGet(Routes.SingleMember)]
    public async Task<ActionResult<MemberResult>> GetMember([FromRoute]GetMemberRequest request)
    {
        var query = this._mapper.Map<GetMemberByIdQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result);
    }
    
    [HttpPut(Routes.SingleMember)]
    public async Task<ActionResult<Guid>> UpdateMember([FromBody]UpdateMemberDataRequest request)
    {
        return await this.SendCommandRequest<ModifyMemberContactDataCommand>(request);
    }
    [HttpPut(Routes.SingleMemberType)]
    public async Task<ActionResult<Guid>> ChangeMemberType([FromBody]ChangeMemberTypeRequest request)
    {
        if(request.MemberType.Type == 1){
            return await this.SendCommandRequest<MakeMemberRegularCommand>(request);
        }

        if(request.MemberType.Type == 2){
            return await this.SendCommandRequest<MakeMemberHonoraryCommand>(request);
        }

        return this.BadRequest();
    }

    
    [HttpGet(Routes.SingleMemberFees)]
    public async Task<ActionResult<PageableResult<MemberFee>>> GetMemberFees([FromRoute]GetMemberFeesRequest request)
    {
        var query = this._mapper.Map<GetMemberFeesQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result.FromQueryResult(query));
    }
    
    [HttpPut(Routes.SingleMemberFee)]
    public async Task<ActionResult<Guid>> RegisterMembershipFee([FromBody]RegisterMembershipFeePaymentRequest request)
    {
        return await this.SendCommandRequest<RegisterMemberFeePaymentCommand>(request);
    }
    
    [HttpPost(Routes.SingleMemberSuspensions)]
    public async Task<ActionResult<Guid>> SuspendMember([FromBody] MemberSuspensionRequest request)
    {
        return await this.SendCommandRequest<SuspendMemberCommand>(request);
    }
    
    [HttpGet(Routes.SingleMemberSuspensions)]
    public async Task<ActionResult<PageableResult<MemberSuspensionResult>>> SuspensionHistory([FromBody] GetMemberSuspensionsRequest request)
    {
        var query = this._mapper.Map<GetMemberSuspensionsQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result.FromQueryResult(query));
    }
    
    [HttpPost(Routes.SingleMemberSuspensionsAppeal)]
    public async Task<ActionResult<Guid>> AppealSuspension([FromBody] MemberSuspensionAppealRequest request)
    {
        return await this.SendCommandRequest<AppealMemberSuspensionCommand>(request);
    }
    
    [HttpPut(Routes.SingleMemberSuspensionsAppeal)]
    public async Task<ActionResult<Guid>> AcceptAppealSuspension([FromBody] AcceptMemberSuspensionAppealRequest request)
    {
        return await this.SendCommandRequest<AcceptSuspensionAppealCommand>(request);
    }
    
    [HttpPatch(Routes.SingleMemberSuspensionsAppeal)]
    public async Task<ActionResult<Guid>> DismissSuspension([FromBody] RejectMemberSuspensionAppealRequest request)
    {
        return await this.SendCommandRequest<DismissSuspensionAppealCommand>(request);
    }
    
    [HttpPost(Routes.SingleMemberExpels)]
    public async Task<ActionResult<Guid>> ExpelsMember([FromBody] MemberExpelRequest request)
    {
        return await this.SendCommandRequest<ExpelMemberCommand>(request);
    }
    
    [HttpGet(Routes.SingleMemberExpels)]
    public async Task<ActionResult<PageableResult<MemberExpelResult>>> ExpelHistory([FromBody] GetMemberExpelsRequest request)
    {
        var query = this._mapper.Map<GetMemberExpelsQuery>(request);
        var result = await this._mediator.Send(query);
        return this.Ok(result.FromQueryResult(query));
    }
    
    [HttpPost(Routes.SingleMemberExpelsAppeal)]
    public async Task<ActionResult<Guid>> AppealExpel([FromBody] MemberExpelAppealRequest request)
    {
        return await this.SendCommandRequest<AppealMemberExpelCommand>(request);
    }
    
    [HttpPut(Routes.SingleMemberExpelsAppeal)]
    public async Task<ActionResult<Guid>> AcceptAppealExpel([FromBody] AcceptMemberExpelAppealRequest request)
    {
        return await this.SendCommandRequest<AcceptExpelAppealCommand>(request);
    }
    
    [HttpPut(Routes.SingleMemberExpiration)]
    public async Task<ActionResult<Guid>> ExpireMembership([FromBody] MemberExpirationRequest request)
    {
        return await this.SendCommandRequest<ExpireMembershipCommand>(request);
    }
    
    [HttpPatch(Routes.SingleMemberExpelsAppeal)]
    public async Task<ActionResult<Guid>> DismissAppealExpel([FromBody] RejectMemberExpelAppealRequest request)
    {
        return await this.SendCommandRequest<DismissExpelAppealCommand>(request);
    }
    
    private async Task<ActionResult<Guid>> SendCommandRequest<TCommand>(MultiTenantRequest request) where TCommand:ApplicationCommandBase
    {
        var decoratedRequest = this.DecorateRequest(request);
        TCommand command = this._mapper.Map<TCommand>(decoratedRequest);
        var result = await this._mediator.Send(command);
        if (result.IsSuccess)
        {
            return this.Ok(result.AggregateId);
        }

        if(!result.IsValid)
        {
            return this.BadRequest(result.Errors);
        }

        return this.UnprocessableEntity(result.Errors);
    }
    
    private dynamic DecorateRequest(MultiTenantRequest request)
    {
        return request.WithTenant(this._identityService.GetUserOrganization());
    }
}