using Microsoft.AspNetCore.Mvc;
using ONPA.Apply.Contract.Requests.Dtos;
using ONPA.Common.Infrastructure;

namespace ONPA.Apply.Contract.Requests;

public record EndorseRecommendationRequest([FromRoute]Guid ApplicationId, [FromRoute]Guid RecommendationId, [FromBody]RecommendationEndorsement Endorsement):MultiTenantRequest;