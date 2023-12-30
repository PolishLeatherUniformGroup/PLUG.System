using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Api;

public class request
{
    [FromRoute]
    public int Id { get; set; }
}