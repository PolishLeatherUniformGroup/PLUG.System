using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api__OLD.Requests.Apply;

public record RefuseRecommendationRequest([FromRoute]Guid ApplicationId, [FromRoute]Guid RecommendationId, [FromBody]RecommendationRefusal Refusal);