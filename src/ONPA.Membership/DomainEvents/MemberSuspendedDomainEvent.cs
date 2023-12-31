﻿using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberSuspendedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public string Justification { get; private set; }
    public DateTime SuspensionDate { get; private set; }
    public DateTime SuspendedUntil { get; private set; }
    public DateTime AppealDeadline { get; private set; }

    public MemberSuspendedDomainEvent(string firstName, string email, string justification,
        DateTime suspensionDate,
        DateTime suspendedUntil,
        DateTime appealDeadline)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.Justification = justification;
        this.SuspensionDate = suspensionDate;
        this.SuspendedUntil = suspendedUntil;
        this.AppealDeadline = appealDeadline;
    }

    private MemberSuspendedDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email,
        string justification,
        DateTime suspensionDate,
        DateTime suspendedUntil,
        DateTime appealDeadline) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.Justification = justification;
        this.SuspensionDate = suspensionDate;
        this.SuspendedUntil = suspendedUntil;
        this.AppealDeadline = appealDeadline;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberSuspendedDomainEvent(aggregateId,tenantId, this.FirstName, this.Email, this.Justification, this.SuspensionDate, this.SuspendedUntil, this.AppealDeadline);
    }
}