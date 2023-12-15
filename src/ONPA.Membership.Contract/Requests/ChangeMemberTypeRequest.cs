using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record ChangeMemberTypeRequest([FromRoute]Guid MemberId, MemberType MemberType);