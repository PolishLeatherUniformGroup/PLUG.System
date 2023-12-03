﻿using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberLeftDomainEvent : DomainEventBase
{
    public CardNumber CardNumber { get; private set; }
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime LeaveDate { get; private set; }
    public List<Guid> Groups { get; private set; }

    public MemberLeftDomainEvent( CardNumber cardNumber, string firstName, string email, DateTime leaveDate,
        List<Guid> groups)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.Email = email;
        this.LeaveDate = leaveDate;
        this.Groups = groups;
    }

    private MemberLeftDomainEvent(Guid aggregateId, CardNumber cardNumber, string firstName, string email, DateTime leaveDate,
        List<Guid> groups) : base(aggregateId)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.Email = email;
        this.LeaveDate = leaveDate;
        this.Groups = groups;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberLeftDomainEvent(aggregateId, this.CardNumber, this.FirstName, this.Email, this.LeaveDate, this.Groups);
    }
}