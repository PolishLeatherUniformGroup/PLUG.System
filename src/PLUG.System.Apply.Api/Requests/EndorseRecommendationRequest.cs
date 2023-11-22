using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api.Requests;

public record EndorseRecommendationRequest([FromRoute]Guid ApplicationId, [FromRoute]Guid RecommendationId, [FromBody]RecommendationEndorsement Endorsement);