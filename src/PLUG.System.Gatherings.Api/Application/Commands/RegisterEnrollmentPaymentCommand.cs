using PLUG.System.Common.Application;
using PLUG.System.SharedDomain;

namespace PLUG.System.Gatherings.Api.Application.Commands;

public sealed record RegisterEnrollmentPaymentCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }
    public Guid EnrollmentId { get; init; }
    public DateTime PaidDate { get; init; }
    public Money PaidAmount { get; init; }

    public RegisterEnrollmentPaymentCommand(Guid publicGatheringId, Guid enrollmentId, DateTime paidDate, Money paidAmount)
    {
        this.PublicGatheringId = publicGatheringId;
        this.EnrollmentId = enrollmentId;
        this.PaidDate = paidDate;
        this.PaidAmount = paidAmount;
    }
}

public sealed record RefundCancelledEnrollmentCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }
    public Guid EnrollmentId { get; init; }
    public Money RefundedAmount { get; init; }
    public DateTime RefundDate { get; init; }

    public RefundCancelledEnrollmentCommand(Guid publicGatheringId, Guid enrollmentId, Money refundedAmount, DateTime refundDate)
    {
        this.PublicGatheringId = publicGatheringId;
        this.EnrollmentId = enrollmentId;
        this.RefundedAmount = refundedAmount;
        this.RefundDate = refundDate;
    }
}