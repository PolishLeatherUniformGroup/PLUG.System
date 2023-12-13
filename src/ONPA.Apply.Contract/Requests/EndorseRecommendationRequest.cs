using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record EndorseRecommendationRequest([FromRoute]Guid ApplicationId, [FromRoute]Guid RecommendationId, [FromBody]RecommendationEndorsement Endorsement);