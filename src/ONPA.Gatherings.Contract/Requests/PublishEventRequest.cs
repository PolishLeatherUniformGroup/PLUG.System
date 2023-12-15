using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record PublishEventRequest([FromRoute]Guid EventId);