using Microsoft.AspNetCore.Mvc;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record ChangeMemberTypeRequest([FromRoute]Guid MemberId, MemberType MemberType);