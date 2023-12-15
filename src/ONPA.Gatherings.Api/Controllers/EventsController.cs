using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Contract;
using ONPA.Gatherings.Contract.Requests;

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

    public EventsController(IMediator mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateEvent([FromBody] CreateEventRequest request)
    {
        return await this.SendCommandRequest<CreatePublicGatheringCommand>(request);
    }
    
    [HttpPut(Routes.SingleEvent)]
    public async Task<ActionResult<Guid>> UpdateEvent([FromBody] UpdateEventDescriptionRequest request)
    {
        return await this.SendCommandRequest<ModifyPublicGatheringDescriptionCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventPrice)]
    public async Task<ActionResult<Guid>> UpdateEventPrice([FromBody] UpdateEventPriceRequest request)
    {
        return await this.SendCommandRequest<ModifyPublicGatheringPriceCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventCapacity)]
    public async Task<ActionResult<Guid>> UpdateEventCapacity([FromBody] UpdateEventCapacityRequest request)
    {
        return await this.SendCommandRequest<ModifyPublicGatheringCapacityCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventSchedule)]
    public async Task<ActionResult<Guid>> UpdateEventSchedule([FromBody] UpdateEventScheduleRequest request)
    {
        return await this.SendCommandRequest<ModifyPublicGatheringScheduleCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventCancellation)]
    public async Task<ActionResult<Guid>> CancelEvent([FromBody] CancelEventRequest request)
    {
        return await this.SendCommandRequest<CancelPublicGatheringCommand>(request);
    }
    [HttpPut(Routes.SingleEventPublication)]
    public async Task<ActionResult<Guid>> PublishEvent([FromBody] PublishEventRequest request)
    {
        return await this.SendCommandRequest<PublishPublicGatheringCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventAcceptance)]
    public async Task<ActionResult<Guid>> AcceptEvent([FromBody] AcceptEventRequest request)
    {
        return await this.SendCommandRequest<AcceptPublicGatheringCommand>(request);
    }
    
    [HttpDelete(Routes.SingleEvent)]
    public async Task<ActionResult<Guid>> ArchiveEvent([FromBody] ArchiveEventRequest request)
    {
        return await this.SendCommandRequest<ArchivePublicGatheringCommand>(request);
    }

    
    [HttpPost(Routes.SingleEventEnrollments)]
    public async Task<ActionResult<Guid>> EnrollToEvent([FromBody] CreateEventEnrollmentRequest request)
    {
        return await this.SendCommandRequest<EnrollToPublicGatheringCommand>(request);
    }
    
    [HttpPut(Routes.SingleEventEnrollmentPayments)]
    public async Task<ActionResult<Guid>> EnrollToEvent([FromBody] RegisterEnrollmentPayment request)
    {
        return await this.SendCommandRequest<EnrollToPublicGatheringCommand>(request);
    }
    [HttpPut(Routes.SingleEventEnrollmentPaymentRefund)]
    public async Task<ActionResult<Guid>> EnrollToEvent([FromBody] RefundEnrollmentPayment request)
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
        var command = this._mapper.Map<TCommand>(request);
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
}
