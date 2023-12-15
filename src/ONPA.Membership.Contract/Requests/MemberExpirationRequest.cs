using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record MemberExpirationRequest([FromRoute]Guid MemberId, [FromBody]MemberExpiration Expiration);