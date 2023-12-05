using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Apply.Api.Application.Commands;

public record ValidateApplicationCommand : ApplicationCommandBase
{
    public Guid ApplicationId { get; init; }
    public IEnumerable<(string MemberNumber, Guid? MemberId)> Recommenders { get; init; }
    public Money YearlyFee { get; init; }

    public ValidateApplicationCommand(Guid applicationId, IEnumerable<(string MemberNumber, Guid? MemberId)> recommenders, Money yearlyFee)
    {
        this.ApplicationId = applicationId;
        this.Recommenders = recommenders;
        this.YearlyFee = yearlyFee;
    }
}