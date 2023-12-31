﻿using ONPA.Common.Domain;

namespace ONPA.Membership.Domain;

public class MembershipSuspension : ValueObject
{
    public DateTime SuspensionDate { get; private set; }
    public DateTime SuspendedUntil { get; private set; }
    public string SuspensionJustification { get; private set; }
    public DateTime AppealDeadline { get; private set; }
    
    public DateTime? AppealDate { get; private set; }
    public string? AppealJustification{ get; private set; }

    public DateTime? AppealDecisionDate { get; private set; }
    public bool? AppealAccepted { get; private set; }
    public string? AppealDecisionJustification { get; private set; }


    private MembershipSuspension(DateTime suspensionDate, DateTime suspendedUntil, string suspensionJustification,
        DateTime appealDeadline,
        DateTime? appealDate = null,
        string? appealJustification = null,
        DateTime? appealDecisionDate = null,
        bool? appealAccepted = null,
        string? appealDecisionJustification = null)
    {
        this.SuspensionDate = suspensionDate;
        this.SuspendedUntil = suspendedUntil;
        this.SuspensionJustification = suspensionJustification;
        this.AppealDeadline = appealDeadline;
        this.AppealDate = appealDate;
        this.AppealJustification = appealJustification;
        this.AppealDecisionDate = appealDecisionDate;
        this.AppealAccepted = appealAccepted;
        this.AppealDecisionJustification = appealDecisionJustification;
    }

    internal MembershipSuspension(DateTime suspensionDate, DateTime suspendedUntil, string suspensionJustification,
        DateTime appealDeadline)
    {
        this.SuspensionDate = suspensionDate;
        this.SuspendedUntil = suspendedUntil;
        this.SuspensionJustification = suspensionJustification;
        this.AppealDeadline = appealDeadline;
    }

    internal MembershipSuspension Appeal(DateTime appealDate, string justification) =>
        new(this.SuspensionDate, this.SuspendedUntil, this.SuspensionJustification, this.AppealDeadline,
            appealDate:appealDate, appealJustification:justification);
    
    internal MembershipSuspension RejectAppeal(DateTime appealDecisionDate, string justification) =>
        new(this.SuspensionDate, this.SuspendedUntil, this.SuspensionJustification, this.AppealDeadline, this.AppealDate, this.AppealJustification,
            appealDecisionDate,
            false,
            justification);
    internal MembershipSuspension AcceptAppeal(DateTime appealDecisionDate, string justification) =>
        new(this.SuspensionDate, this.SuspendedUntil, this.SuspensionJustification, this.AppealDeadline, this.AppealDate, this.AppealJustification,
            appealDecisionDate,
            true,
            justification);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.SuspensionDate;
        yield return this.SuspendedUntil;
        yield return this.SuspensionJustification;
        yield return this.AppealDeadline;
        yield return this.AppealDate;
        yield return this.AppealJustification;
        yield return this.AppealDecisionDate;
        yield return this.AppealAccepted;
        yield return this.AppealDecisionJustification;
    }
}