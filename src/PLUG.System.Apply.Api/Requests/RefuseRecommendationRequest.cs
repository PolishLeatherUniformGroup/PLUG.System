using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api.Requests;

public record RefuseRecommendationRequest([FromRoute]Guid ApplicationId, [FromRoute]Guid RecommendationId, [FromBody]RecommendationRefusal Refusal);