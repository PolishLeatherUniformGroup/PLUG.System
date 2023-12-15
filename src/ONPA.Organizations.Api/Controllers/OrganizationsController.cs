using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Application;
using ONPA.Common.Queries;
using ONPA.Organizations.Api.Application.Commands;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Contract;
using ONPA.Organizations.Contract.Requests;
using ONPA.Organizations.Contract.Responses;

namespace ONPA.Organizations.Api.Controllers
{
    [Route("api/organizations")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrganizationsController(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrganization([FromBody] CreateOrganizationRequest request)
        {
            return await this.SendCommandRequest<CreateOrganizationCommand>(request);
        }
        
        [HttpGet]
        public async Task<ActionResult<PageableResult<OrganizationResponse>>> GetOrganizations([FromQuery] GetOrganizationsRequest request)
        {
            var query = this._mapper.Map<GetOrganizationsQuery>(request);
            var result = await this._mediator.Send(query);
            return this.Ok(result.FromQueryResult(query));
        }
        
        [HttpPut(Routes.SingleOrganization)]
        public async Task<ActionResult<Guid>> UpdateOrganizationData([FromBody] UpdateOrganizationDataRequest request)
        {
            return await this.SendCommandRequest<ChangeOrganizationDataCommand>(request);
        }
        
        [HttpGet(Routes.SingleOrganization)]
        public async Task<ActionResult<OrganizationResponse?>> GetOrganization([FromRoute]GetOrganizationRequest request)
        {
            var query = this._mapper.Map<GetOrganizationQuery>(request);
            var result = await this._mediator.Send(query);
            if(result == null)
            {
                return this.NotFound();
            }
            return this.Ok(result);
        }
        
        [HttpPut(Routes.SingleOrganizationSettings)]
        public async Task<ActionResult<Guid>> UpdateSettingsData([FromBody] UpdateOrganizationSettingsRequest request)
        {
            return await this.SendCommandRequest<UpdateOrganizationSettingsCommand>(request);
        }
        
        [HttpGet(Routes.SingleOrganizationSettings)]
        public async Task<ActionResult<OrganizationSettingsResponse?>> GetOrganizationSettings([FromRoute] GetOrganizationSettingsRequest request)
        {
            var query = this._mapper.Map<GetOrganizationSettingsQuery>(request);
            var result = await this._mediator.Send(query);
            if(result == null)
            {
                return this.NotFound();
            }
            return this.Ok(result);
        }
        
        [HttpPost(Routes.SingleOrganizationFees)]
        public async Task<ActionResult<Guid>> AddMembershipFee([FromBody] AddMembershipFeeRequest request)
        {
            return await this.SendCommandRequest<RequestMembershipFeeCommand>(request);
        }
        
        [HttpGet(Routes.SingleOrganizationFees)]
        public async Task<ActionResult<PageableResult<OrganizationFeeResponse>>> GetOrganizationFees([FromRoute] GetOrganizationFeesRequest request)
        {
            var query = this._mapper.Map<GetOrganizationFeesQuery>(request);
            var result = await this._mediator.Send(query);
            return this.Ok(result.FromQueryResult(query));
        }
        
        [HttpGet(Routes.SingleOrganizationFeeForYear)]
        public async Task<ActionResult<OrganizationFeeResponse?>> GetOrganizationFeeForYear([FromRoute] GetOrganizationFeeForYearRequest request)
        {
            var query = this._mapper.Map<GetOrganizationFeeForYearQuery>(request);
            var result = await this._mediator.Send(query);
            if(result == null)
            {
                return this.NotFound();
            }
            return this.Ok(result);
        }

        private async Task<ActionResult<Guid>> SendCommandRequest<TCommand>(dynamic request)
            where TCommand : ApplicationCommandBase
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
}