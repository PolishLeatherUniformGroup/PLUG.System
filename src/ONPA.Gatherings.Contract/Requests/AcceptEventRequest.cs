using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record AcceptEventRequest([FromRoute]Guid EventId);