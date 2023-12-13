using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONPA.Apply.Api.Application.Commands;
using ONPA.Apply.Api.Application.Queries;
using ONPA.Apply.Contract;
using ONPA.Apply.Contract.Requests;
using ONPA.Apply.Contract.Responses;
using ONPA.Common.Application;
using ONPA.Common.Queries;

namespace ONPA.Apply.Api.Controllers
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

        public ApplicationsController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }
        
        [HttpPost("")]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateApplicationRequest request)
        {
            return await this.SendCommandRequest<CreateApplicationFormCommand>(request);
        }

        [HttpPut("{applicationId}/recommendations/{recommendationId}")]
        public async Task<ActionResult<Guid>> EndorseRecommendation(EndorseRecommendationRequest request)
        {
            return await this.SendCommandRequest<EndorseApplicationRecommendationCommand>(request);
        }
        [HttpDelete("{applicationId}/recommendations/{recommendationId}")]
        public async Task<ActionResult<Guid>> RefuseRecommendation(EndorseRecommendationRequest request)
        {
            return await this.SendCommandRequest<RefuseApplicationRecommendationCommand>(request);
        }

        [HttpPut(Routes.SingleApplication)]
        public async Task<ActionResult<Guid>> ApproveApplication(ApproveApplicationRequest request)
        {
           return await this.SendCommandRequest<ApproveApplicationCommand>(request);
        }
        
        [HttpPatch(Routes.SingleApplication)]
        public async Task<ActionResult<Guid>> RejectApplication(RejectApplicationRequest request)
        {
            return await this.SendCommandRequest<RejectApplicationCommand>(request);
        }

        [HttpPost(Routes.SingleApplicationPayments)]
        public async Task<ActionResult<Guid>> RegisterPayment(RegisterApplicationPaymentRequest request)
        {
            return await this.SendCommandRequest<RegisterApplicationFeePaymentCommand>(request);
        }
        
        [HttpPost(Routes.SingleApplicationAppeals)]
        public async Task<ActionResult<Guid>> AppealRejection(AppealApplicationRejectionRequest request)
        {
            return await this.SendCommandRequest<AppealApplicationRejectionCommand>(request);
        }
        
        [HttpPut(Routes.SingleApplicationAppeals)]
        public async Task<ActionResult<Guid>> ApproveAppealRejection(ApproveAppealApplicationRejectionRequest request)
        {
            return await this.SendCommandRequest<ApproveApplicationRejectionAppealCommand>(request);
        }
        [HttpPatch(Routes.SingleApplicationAppeals)]
        public async Task<ActionResult<Guid>> RejectAppealRejection(RejectAppealApplicationRejectionRequest request)
        {
            return await this.SendCommandRequest<DismissApplicationRejectionAppealCommand>(request);
        }
        
        [HttpGet]
        public async Task<ActionResult<PageableResult<ApplicationResult>>> GetApplications([FromQuery]GetApplicationsRequest request)
        {
            var query = this._mapper.Map<GetApplicationsByStatusQuery>(request);
            var result = await this._mediator.Send(query);
            return this.Ok(result.FromQueryResult(query));
        } 
        
        [HttpGet(Routes.SingleApplication)]
        public async Task<ActionResult<ApplicationDetails>> GetApplication([FromRoute]GetApplicationRequest request)
        {
            var query = this._mapper.Map<GetApplicationQuery>(request);
            var result = await this._mediator.Send(query);
            return this.Ok(result);
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
