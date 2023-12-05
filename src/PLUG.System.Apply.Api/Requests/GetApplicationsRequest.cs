using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api.Requests;

public record GetApplicationsRequest([FromQuery] int Status, [FromQuery] int Page = 0, [FromQuery] int Limit = 10);