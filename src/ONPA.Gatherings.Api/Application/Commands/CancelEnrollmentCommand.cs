using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record CancelEnrollmentCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }
    public Guid EnrollmentId { get; init; }
    public Money RefundableAmount { get; init; }
    public DateTime CancellationDate { get; init; }

    public CancelEnrollmentCommand(Guid publicGatheringId, Guid enrollmentId, Money refundableAmount, DateTime cancellationDate)
    {
        this.PublicGatheringId = publicGatheringId;
        this.EnrollmentId = enrollmentId;
        this.RefundableAmount = refundableAmount;
        this.CancellationDate = cancellationDate;
    }
}