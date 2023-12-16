using Microsoft.AspNetCore.Mvc;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record MemberExpirationRequest([FromRoute]Guid MemberId, [FromBody]MemberExpiration Expiration);