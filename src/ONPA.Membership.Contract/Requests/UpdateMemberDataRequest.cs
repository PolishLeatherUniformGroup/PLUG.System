using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record UpdateMemberDataRequest(Guid MemberId, MemberContactData ContactData);