using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Api.Requests;

public record GetApplicationsRequest([FromQuery] int Status, [FromQuery] int Page = 0, [FromQuery] int Limit = 10);