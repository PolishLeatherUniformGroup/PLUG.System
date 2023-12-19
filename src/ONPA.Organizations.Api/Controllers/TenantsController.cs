using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Contract.Responses;

namespace ONPA.Organizations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TenantInfo>> GetTenant(string id)
        {
            var query = new GetOrganizationBySlugQuery(id);
            var result = await this._mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
