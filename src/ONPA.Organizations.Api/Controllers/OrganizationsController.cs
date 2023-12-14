using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Application;
using ONPA.Organizations.Api.Application.Commands;
using ONPA.Organizations.Contract.Requests;

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

        [HttpPut("{organizationId}/data")]
        public async Task<ActionResult<Guid>> UpdateOrganizationData([FromBody] UpdateOrganizationDataRequest request)
        {
            return await this.SendCommandRequest<ChangeOrganizationDataCommand>(request);
        }
        [HttpPut("{organizationId}/settings")]
        public async Task<ActionResult<Guid>> UpdateSettingsData([FromBody] UpdateOrganizationSettingsRequest request)
        {
            return await this.SendCommandRequest<UpdateOrganizationSettingsCommand>(request);
        }
        
        [HttpPost("{organizationId}/fees")]
        public async Task<ActionResult<Guid>> AddMembershipFee([FromBody] AddMembershipFeeRequest request)
        {
            return await this.SendCommandRequest<RequestMembershipFeeCommand>(request);
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