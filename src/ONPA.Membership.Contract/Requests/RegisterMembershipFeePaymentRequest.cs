using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record RegisterMembershipFeePaymentRequest([FromRoute]Guid MemberId, [FromRoute] Guid FeeId, [FromBody] Payment Payment);
public record MemberSuspensionRequest([FromRoute] Guid MemberId, [FromBody] MemberSuspension Suspension);
public record MemberSuspensionAppealRequest([FromRoute] Guid MemberId, [FromBody] SuspensionAppeal Appeal);
public record AcceptMemberSuspensionAppealRequest([FromRoute] Guid MemberId, [FromBody] SuspensionAppealDecision Decision);
public record RejectMemberSuspensionAppealRequest([FromRoute] Guid MemberId, [FromBody] SuspensionAppealDecision Decision);
public record MemberSuspension(DateTime SuspensionDate, DateTime? ReinstatementDate, string Justification, int DaysToAppeal);
public record SuspensionAppeal(DateTime AppealDate,  string Justification);
public record SuspensionAppealDecision(DateTime DecisionDate,  string Justification);

public record MemberExpelRequest([FromRoute] Guid MemberId, [FromBody] MemberExpel Expel);

public record MemberExpelAppealRequest([FromRoute] Guid MemberId, [FromBody] ExpelAppeal Appeal);
public record AcceptMemberExpelAppealRequest([FromRoute] Guid MemberId, [FromBody] ExpelAppealDecision Decision);
public record RejectMemberExpelAppealRequest([FromRoute] Guid MemberId, [FromBody] ExpelAppealDecision Decision);
public record MemberExpel(DateTime ExpelDate,  string Justification, int DaysToAppeal);
public record ExpelAppeal(DateTime AppealDate,  string Justification);
public record ExpelAppealDecision(DateTime DecisionDate,  string Justification);