using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record RefuseRecommendationRequest([FromRoute]Guid ApplicationId, [FromRoute]Guid RecommendationId, [FromBody]RecommendationRefusal Refusal);