using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api__OLD.Requests.Apply;

public record EndorseRecommendationRequest([FromRoute]Guid ApplicationId, [FromRoute]Guid RecommendationId, [FromBody]RecommendationEndorsement Endorsement);