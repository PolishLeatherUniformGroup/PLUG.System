using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record GetMemberRequest([FromRoute] Guid MemberId);