using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PLUG.System.Apply.Api.Requests;
using PLUG.System.Common.Application;

namespace PLUG.System.Apply.Api.Controllers
{
    
    [Route("api/applications")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ApplicationsController()
        {
            //this._mediator = mediator;
            //this._mapper = mapper;
        }
        
        [HttpPost("")]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateApplicationRequest request)
        {
            return this.Ok();
            //return await this.SendCommandRequest<CreateApplicationFormCommand>(request);
        }

        [HttpPut("{applicationId}/recommendations/{recommendationId}")]
        public async Task<ActionResult<Guid>> EndorseRecommendation(EndorseRecommendationRequest request)
        {
            return this.Ok();
            //return await this.SendCommandRequest<EndorseApplicationRecommendationCommand>(request);
        }
        [HttpDelete("{applicationId}/recommendations/{recommendationId}")]
        public async Task<ActionResult<Guid>> RefuseRecommendation(EndorseRecommendationRequest request)
        {
            return this.Ok();
            //return await this.SendCommandRequest<RefuseApplicationRecommendationCommand>(request);
        }

        [HttpPut("{applicationId}")]
        public async Task<ActionResult<Guid>> ApproveApplication(ApproveApplicationRequest request)
        {
            return this.Ok();
            //return await this.SendCommandRequest<ApproveApplicationCommand>(request);
        }
        
        [HttpDelete("{applicationId}")]
        public async Task<ActionResult<Guid>> RejectApplication(RejectApplicationRequest request)
        {
            return this.Ok();
            //return await this.SendCommandRequest<RejectApplicationCommand>(request);
        }

        [HttpPut("{applicationId}/payments")]
        public async Task<ActionResult<Guid>> RegisterPayment(RegisterApplicationPaymentRequest request)
        {
            return this.Ok();
            //return await this.SendCommandRequest<RegisterApplicationFeePaymentCommand>(request);
        }
        
        [HttpPost("{applicationId}/appeals")]
        public async Task<ActionResult<Guid>> AppealRejection(AppealApplicationRejectionRequest request)
        {
            return this.Ok();
            //return await this.SendCommandRequest<AppealApplicationRejectionCommand>(request);
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
}
