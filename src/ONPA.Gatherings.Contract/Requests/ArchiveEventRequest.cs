using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record ArchiveEventRequest([FromRoute]Guid EventId);