using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Application;
using ONPA.Common.Infrastructure;
using ONPA.Common.Queries;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Api.Services;
using ONPA.Gatherings.Contract;
using ONPA.Gatherings.Contract.Requests;
using ONPA.Gatherings.Contract.Responses;

namespace ONPA.Gatherings.Api.Controllers;

[Route("api/events")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public EventsController(IMediator mediator, IMapper mapper, IIdentityService identityService)
    {
        this._mediator = mediator;
        this._mapper = mapper;
        this._identityService = identityService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateEvent([FromBody] CreateEventRequest request)
    {
        return await this.SendCommandRequest<CreateEventCommand>(request);
    }
    
    [HttpGet]
    public async Task<ActionResult<PageableResult<EventResponse>>> GetEvents([FromQuery] GetEventsByStatusRequest request)
    {
        var query = this._mapper.Map<GetEventsByStatusQuery>(request);
        var response = await this._mediator.Send(query);
        return this.Ok(response.FromQueryResult(query));
    }
    
    [HttpPut(Routes.SingleEvent)]
    public async Task<ActionResult<Guid>> UpdateEvent([FromBody] UpdateEventDescriptionRequest request)
    {
        return await this.SendCommandRequest<ModifyEventDescriptionCommand>(request);
    }
    
    [HttpGet(Routes.SingleEvent)]
    public async Task<ActionResult<EventResponse>> GetEvent([FromRoute] GetEventRequest request)
    {
        var query = this._mapper.Map<GetEventQuery>(request);
        var response = await this._mediator.Send(query);
        return this.Ok(response);
    }

    
    [HttpPut(Routes.SingleEventPrice)]
    public async Task<ActionResult<Guid>> UpdateEventPrice([FromBody] UpdateEventPriceRequest request)
    {
        return await this.SendCommandRequest<ModifyEventPriceCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventCapacity)]
    public async Task<ActionResult<Guid>> UpdateEventCapacity([FromBody] UpdateEventCapacityRequest request)
    {
        return await this.SendCommandRequest<ModifyEventCapacityCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventSchedule)]
    public async Task<ActionResult<Guid>> UpdateEventSchedule([FromBody] UpdateEventScheduleRequest request)
    {
        return await this.SendCommandRequest<ModifyEventScheduleCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventCancellation)]
    public async Task<ActionResult<Guid>> CancelEvent([FromBody] CancelEventRequest request)
    {
        return await this.SendCommandRequest<CancelEventCommand>(request);
    }
    [HttpPut(Routes.SingleEventPublication)]
    public async Task<ActionResult<Guid>> PublishEvent([FromBody] PublishEventRequest request)
    {
        return await this.SendCommandRequest<PublishEventCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventAcceptance)]
    public async Task<ActionResult<Guid>> AcceptEvent([FromBody] AcceptEventRequest request)
    {
        return await this.SendCommandRequest<AcceptEventCommand>(request);
    }
    
    [HttpDelete(Routes.SingleEvent)]
    public async Task<ActionResult<Guid>> ArchiveEvent([FromBody] ArchiveEventRequest request)
    {
        return await this.SendCommandRequest<ArchiveEventCommand>(request);
    }
    
    [HttpPost(Routes.SingleEventEnrollments)]
    public async Task<ActionResult<Guid>> EnrollToEvent([FromBody] CreateEventEnrollmentRequest request)
    {
        return await this.SendCommandRequest<EnrollToEventCommand>(request);
    }
    
    [HttpGet(Routes.SingleEventEnrollments)]
    public async Task<ActionResult<PageableResult<EnrollmentResponse>>> GetEventEnrollments([FromRoute] GetEventEnrollmentsRequest request)
    {
        var query = this._mapper.Map<GetEventEnrollmentsQuery>(request);
        var response = await this._mediator.Send(query);
        return this.Ok(response.FromQueryResult(query));
    }
    
    [HttpGet(Routes.SingleEventParticipants)]
    public async Task<ActionResult<PageableResult<ParticipantResponse>>> GetEventParticipants([FromRoute] GetEventParticipantsRequest request)
    {
        var query = this._mapper.Map<GetEventParticipantsQuery>(request);
        var response = await this._mediator.Send(query);
        return this.Ok(response.FromQueryResult(query));
    }
    
    [HttpPut(Routes.SingleEventEnrollmentPayments)]
    public async Task<ActionResult<Guid>> EnrollToEvent([FromBody] RegisterEnrollmentPaymentRequest request)
    {
        return await this.SendCommandRequest<RegisterEnrollmentPaymentCommand>(request);
    }
    [HttpPut(Routes.SingleEventEnrollmentPaymentRefund)]
    public async Task<ActionResult<Guid>> RefundEnrollment([FromBody] RefundEnrollmentPaymentRequest request)
    {
        return await this.SendCommandRequest<RefundCancelledEnrollmentCommand>(request);
    }
    
    [HttpPatch(Routes.SingleEventSingleEnrollment)]
    public async Task<ActionResult<Guid>> CancelEnrollment([FromBody] CancelEnrollmentRequest request)
    {
        return await this.SendCommandRequest<CancelEnrollmentCommand>(request);
    }

    private async Task<ActionResult<Guid>> SendCommandRequest<TCommand>(dynamic request) where TCommand : ApplicationCommandBase
    {
        var decoratedRequest = this.DecorateRequest(request);
        TCommand command = this._mapper.Map<TCommand>(decoratedRequest);
        var result = await this._mediator.Send(command);
        if (result.IsSuccess)
        {
            return this.Ok(result.AggreagteId);
        }

        if (!result.IsValid)
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