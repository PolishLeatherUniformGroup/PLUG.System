using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.Domain;

public class MembershipExpel : ValueObject
{
    public DateTime ExpelDate { get; private set; }
    public string ExpelJustification { get; private set; }
    public DateTime AppealDeadline { get; private set; }
    
    public DateTime? AppealDate { get; private set; }
    public string? AppealJustification{ get; private set; }

    public DateTime? AppealDecisionDate { get; private set; }
    public bool? AppealAccepted { get; private set; }
    public string? AppealDecisionJustification { get; private set; }


    private MembershipExpel(DateTime expelDate, string expelJustification,
        DateTime appealDeadline,
        DateTime? appealDate = null,
        string? appealJustification = null,
        DateTime? appealDecisionDate = null,
        bool? appealAccepted = null,
        string? appealDecisionJustification = null)
    {
        this.ExpelDate = expelDate;
        this.ExpelJustification = expelJustification;
        this.AppealDeadline = appealDeadline;
        this.AppealDate = appealDate;
        this.AppealJustification = appealJustification;
        this.AppealDecisionDate = appealDecisionDate;
        this.AppealAccepted = appealAccepted;
        this.AppealDecisionJustification = appealDecisionJustification;
    }

    internal MembershipExpel(DateTime expelDate, string expelJustification,
        DateTime appealDeadline)
    {
        this.ExpelDate = expelDate;
        this.ExpelJustification = expelJustification;
        this.AppealDeadline = appealDeadline;
    }

    internal MembershipExpel Appeal(DateTime appealDate, string justification) =>
        new(this.ExpelDate, this.ExpelJustification, this.AppealDeadline, appealDate, justification);
    
    internal MembershipExpel RejectAppeal(DateTime appealDecisionDate, string justification) =>
        new(this.ExpelDate,
            this.ExpelJustification,
            this.AppealDeadline,
            this.AppealDate,
            this.AppealJustification,
            appealDecisionDate,
            false,
            justification);
    internal MembershipExpel AcceptAppeal(DateTime appealDecisionDate, string justification) =>
        new(this.ExpelDate,
            this.ExpelJustification,
            this.AppealDeadline,
            this.AppealDate,
            this.AppealJustification,
            appealDecisionDate,
            true,
            justification);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.ExpelDate;
        yield return this.ExpelJustification;
        yield return this.AppealDeadline;
        yield return this.AppealDate;
        yield return this.AppealJustification;
        yield return this.AppealDecisionDate;
        yield return this.AppealAccepted;
        yield return this.AppealDecisionJustification;
    }
}