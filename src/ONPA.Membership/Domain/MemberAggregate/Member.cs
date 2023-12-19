using ONPA.Membership.DomainEvents;
using ONPA.Membership.StateEvents;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using PLUG.System.SharedDomain;
using PLUG.System.SharedDomain.Helpers;

namespace ONPA.Membership.Domain;

public sealed partial class Member : MultiTenantAggregateRoot
{
    public CardNumber MemberNumber { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public DateTime JoinDate { get; private set; }
    public DateTime? TerminationDate { get; private set; }
    public DateTime MembershipValidUntil { get; private set; }

    public string? TerminationReason { get; private set; }
    public bool IsValid => DateTime.UtcNow.Date < this.MembershipValidUntil.Date;

    private readonly IList<MembershipFee> _membershipFees = new List<MembershipFee>();
    private MembershipSuspension? _suspension;
    private MembershipExpel? _expel;
    public IEnumerable<MembershipFee> MembershipFees => this._membershipFees;
    public MembershipFee? CurrentFee => this._membershipFees.MaxBy(f => f.FeeEndDate);

    public bool IsFeeBalanced => this.CurrentFee is not null && this.CurrentFee.IsBalanced;

    public MembershipType MembershipType { get; private set; }

    public MembershipStatus Status { get; private set; }

    public MembershipSuspension? Suspension => this._suspension;
    public MembershipExpel? Expel => this._expel;

    private readonly List<Guid> _groupMemberships = new();
    public IEnumerable<Guid> GroupMembership => this._groupMemberships;

    public Member(Guid aggregateId, Guid tenantId, IEnumerable<IStateEvent> changes) : base(aggregateId, tenantId, changes)
    {
    }

    public Member(Guid tenantId, CardNumber cardNumber, string firstName, string lastName, string email, string phone, string address,
        DateTime joinDate,
        Money paidFee):base(tenantId)
    {
        this.MemberNumber = cardNumber;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
        this.Address = address;
        this.JoinDate = joinDate;
        this.Status = MembershipStatus.Active;
        this.MembershipType = MembershipType.Regular;
        this.MembershipValidUntil = joinDate.ToYearEnd();

        var fee = new MembershipFee(paidFee, joinDate, joinDate.ToYearEnd(), paidFee, joinDate);
        this._membershipFees.Add(fee);

        var change = new MemberCreated(cardNumber, this.FirstName, this.LastName, this.Email, this.Phone, this.Address,
            this.JoinDate, fee);
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberJoinedDomainEvent(this.MemberNumber, this.FirstName, this.LastName,
            this.Email,this.Phone);
        this.RaiseDomainEvent(domainEvent);
    }

    public void ModifyContactData(string email, string phone, string address)
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }

        this.Email = email;
        this.Phone = phone;
        this.Address = address;

        var change = new MemberContactDataModified(email, phone, address);
        this.RaiseChangeEvent(change);
    }

    public void MakeHonoraryMember()
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }

        this.MembershipType = MembershipType.Honorary;
        var change = new MemberTypeChanged(this.MembershipType);
        this.RaiseChangeEvent(change);
    }

    public void MakeRegularMember()
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }

        this.MembershipType = MembershipType.Regular;
        var change = new MemberTypeChanged(this.MembershipType);
        this.RaiseChangeEvent(change);
    }

    public void RequestFeePayment(Money feeAmount, DateTime dueDate, DateTime periodEnd)
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }

        if (this._membershipFees.Any(f => f.FeeEndDate == periodEnd))
        {
            return;
        }

        var fee = new MembershipFee(feeAmount, dueDate, periodEnd);
        this._membershipFees.Add(fee);

        var change = new MemberFeeRequested(fee);
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberFeePaymentRequestedDomainEvent(this.FirstName, this.Email,
            fee.DueAmount, fee.DueDate, fee.FeeEndDate);
        this.RaiseDomainEvent(domainEvent);
    }

    public void RegisterPaymentFee(Guid membershipFeeId, Money paidAmount, DateTime paidDate)
    {
        if (this.Status != MembershipStatus.Active && this.Status != MembershipStatus.Expired)
        {
            throw new AggregateInvalidStateException();
        }

        var fee = this._membershipFees.SingleOrDefault(f => f.Id == membershipFeeId);
        if (fee is null)
        {
            throw new EntityNotFoundException();
        }

        fee.AddPayment(paidAmount, paidDate);

        var change = new MemberFeePaid(membershipFeeId, paidAmount, paidDate);
        this.RaiseChangeEvent(change);
        if (!fee.DueAmount.IsZero())
        {
            var registerPaymentDomainEvent =
                new MemberFeePaymentRegisteredDomainEvent(this.FirstName, this.Email, paidAmount, fee.DueAmount,
                    paidDate,
                    fee.FeeEndDate);
            this.RaiseDomainEvent(registerPaymentDomainEvent);
        }

        if (fee.IsBalanced)
        {
            this.MembershipValidUntil = fee.FeeEndDate;
            if (this.Status == MembershipStatus.Expired)
            {
                this.Reactivate();
            }

            var membershipExtended =
                new MembershipExtendedDomainEvent(this.FirstName, this.Email, this.MembershipValidUntil);
            this.RaiseDomainEvent(membershipExtended);
        }
    }

    public void SuspendMember(string justification, DateTime suspensionDate, DateTime suspendUntil, int daysToAppeal)
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }
        
        this._suspension = new MembershipSuspension(suspensionDate, suspendUntil, justification,
            suspensionDate.AddDays(daysToAppeal));
        this.Status = MembershipStatus.Suspended;

        var changeEvent = new MemberSuspended(this._suspension);
        this.RaiseChangeEvent(changeEvent);

        var domainEvent = new MemberSuspendedDomainEvent(this.FirstName, this.Email,
            this._suspension.SuspensionJustification,
            this._suspension.SuspensionDate, this._suspension.SuspendedUntil, this._suspension.AppealDeadline);
        this.RaiseDomainEvent(domainEvent);
    }

    public void AppealSuspension(string justification, DateTime receivedDate)
    {
        if (this.Status != MembershipStatus.Suspended)
        {
            throw new AggregateInvalidStateException();
        }
        
        this._suspension = this._suspension!.Appeal(receivedDate, justification);

        var change = new MemberSuspensionAppealReceived(this._suspension);
        this.RaiseChangeEvent(change);

        if (receivedDate.Date > this._suspension.AppealDeadline.Date)
        {
            this._suspension = this._suspension.RejectAppeal(receivedDate, "Odwołanie wpłynęło po terminie.");

            var autoDecision = new SuspensionAppealDismissed(this._suspension);
            this.RaiseChangeEvent(autoDecision);

            var rejectionEvent = new MemberSuspensionAppealDismissedDomainEvent(this.FirstName, this.Email,
                this._suspension!.AppealDecisionDate.GetValueOrDefault(),
                this._suspension.AppealDecisionJustification!);
            this.RaiseDomainEvent(rejectionEvent);
            return;
        }

        var domainEvent = new MemberSuspensionAppealReceivedDomainEvent(this.FirstName, this.Email,
            this._suspension!.AppealDate.GetValueOrDefault());
        this.RaiseDomainEvent(domainEvent);
    }

    public void AcceptAppealSuspension(DateTime approveDate, string justification)
    {
        if (this.Status != MembershipStatus.Suspended)
        {
            throw new AggregateInvalidStateException();
        }

        if (this._suspension is null ||
            (this._suspension.AppealDate is null && this._suspension.AppealJustification is null))
        {
            throw new AggregateInvalidStateException();
        }

        this._suspension = this._suspension.AcceptAppeal(approveDate, justification);

        var change = new SuspensionAppealApproved(this._suspension);
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberSuspensionAppealApprovedDomainEvent(this.FirstName, this.Email,
            this._suspension.AppealDecisionDate.GetValueOrDefault(), this._suspension.AppealDecisionJustification!);
        this.RaiseDomainEvent(domainEvent);
    }

    public void DismissAppealSuspension(DateTime rejectDate, string justification)
    {
        if (this.Status != MembershipStatus.Suspended)
        {
            throw new AggregateInvalidStateException();
        }

        if (this._suspension is null ||
            (this._suspension.AppealDate is null && this._suspension.AppealJustification is null))
        {
            throw new AggregateInvalidStateException();
        }

        this._suspension = this._suspension.RejectAppeal(rejectDate, justification);

        var change = new SuspensionAppealDismissed(this._suspension);
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberSuspensionAppealDismissedDomainEvent(this.FirstName, this.Email,
            this._suspension.AppealDecisionDate.GetValueOrDefault(), this._suspension.AppealDecisionJustification!);
        this.RaiseDomainEvent(domainEvent);
    }

    /// <summary>
    /// Member can be expelled for breaching rules.
    /// </summary>
    /// <param name="justification"></param>
    /// <param name="expelDate"></param>
    /// <param name="daysToAppeal"></param>
    public void ExpelMember(string justification, DateTime expelDate, int daysToAppeal)
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }


        this._expel = new MembershipExpel(expelDate, justification, expelDate.AddDays(daysToAppeal));
        this.Status = MembershipStatus.Expelled;

        var changeEvent = new MemberExpelled(this._expel);
        this.RaiseChangeEvent(changeEvent);

        var domainEvent = new MemberExpelledDomainEvent(this.FirstName, this.Email, this._expel.ExpelJustification,
            this._expel.ExpelDate, this._expel.AppealDeadline);
        this.RaiseDomainEvent(domainEvent);
    }

    public void AppealExpel(string justification, DateTime receivedDate)
    {
        if (this.Status != MembershipStatus.Expelled)
        {
            throw new AggregateInvalidStateException();
        }

        this._expel = this._expel!.Appeal(receivedDate, justification);

        var change = new MemberExpelAppealReceived(this._expel);
        this.RaiseChangeEvent(change);

        if (receivedDate.Date > this._expel.AppealDeadline.Date)
        {
            this._expel = this._expel.RejectAppeal(receivedDate, "Odwołanie wpłynęło po terminie.");
            this.TerminationDate = this._expel.AppealDeadline;

            var autoDecision = new ExpelAppealDismissed(this._expel, this.TerminationDate.GetValueOrDefault());
            this.RaiseChangeEvent(autoDecision);

            var rejectionEvent = new MemberExpelAppealDismissedDomainEvent(this.MemberNumber,
                this.FirstName, this.Email,
                this.Expel!.AppealDecisionDate.GetValueOrDefault(),
                this.Expel.AppealDecisionJustification!,
                this._groupMemberships);
            this.RaiseDomainEvent(rejectionEvent);
            this.Status = MembershipStatus.Deleted;
            return;
        }

        var domainEvent = new MemberExpelAppealReceivedDomainEvent(this.FirstName, this.Email,
            this._expel!.AppealDate.GetValueOrDefault());
        this.RaiseDomainEvent(domainEvent);
    }

    public void AcceptAppealExpel(DateTime approveDate, string justification)
    {
        if (this.Status != MembershipStatus.Expelled)
        {
            throw new AggregateInvalidStateException();
        }

        if (this._expel is null || (this._expel.AppealDate is null && this._expel.AppealJustification is null))
        {
            throw new AggregateInvalidStateException();
        }

        this._expel = this._expel.AcceptAppeal(approveDate, justification);

        var change = new ExpelAppealApproved(this._expel);
        
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberExpelAppealApprovedDomainEvent(this.FirstName, this.Email,
            this._expel.AppealDecisionDate.GetValueOrDefault(), this._expel.AppealDecisionJustification!);
        this.RaiseDomainEvent(domainEvent);
    }

    public void DismissAppealExpel(DateTime rejectDate, string justification)
    {
        if (this.Status != MembershipStatus.Expelled)
        {
            throw new AggregateInvalidStateException();
        }

        if (this._expel is null || (this._expel.AppealDate is null && this._expel.AppealJustification is null))
        {
            throw new AggregateInvalidStateException();
        }

        this._expel = this._expel.RejectAppeal(rejectDate, justification);
        this.TerminationDate = rejectDate;
        this.Status = MembershipStatus.Deleted;

        var change = new ExpelAppealDismissed(this._expel, rejectDate);
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberExpelAppealDismissedDomainEvent(this.MemberNumber,this.FirstName, this.Email,
            this._expel.AppealDecisionDate.GetValueOrDefault(), this._expel.AppealDecisionJustification!, this._groupMemberships);
        this.RaiseDomainEvent(domainEvent);
    }

    /// <summary>
    /// Member can leave on free will.
    /// </summary>
    public void LeaveOrganization(DateTime leaveDate)
    {
        if (this.Status != MembershipStatus.Active && this.Status != MembershipStatus.Suspended &&
            this.Status != MembershipStatus.Expelled)
        {
            throw new AggregateInvalidStateException();
        }

        this.TerminationDate = leaveDate;
        this.TerminationReason = "Członek zrezygnował.";
        this.Status = MembershipStatus.Leaved;

        var change = new MemberLeft(this.TerminationDate.GetValueOrDefault(), this.TerminationReason);
        this.RaiseChangeEvent(change);

        var domainEvent =
            new MemberLeftDomainEvent(this.MemberNumber,this.FirstName, this.Email, this.TerminationDate.GetValueOrDefault(), this._groupMemberships);
        this.RaiseDomainEvent(domainEvent);
    }

    /// <summary>
    /// Membership expires, when fee is overdue, member dies, or lost rights to be member 
    /// </summary>
    public void MembershipExpired(DateTime expirationDate, string reason)
    {
        if (this.Status != MembershipStatus.Active)
        {
            throw new AggregateInvalidStateException();
        }

        this.TerminationDate = expirationDate;
        this.TerminationReason = reason;
        this.Status = MembershipStatus.Expired;

        var change = new MembershipExpired(this.TerminationDate.GetValueOrDefault(), this.TerminationReason);
        this.RaiseChangeEvent(change);

        var domainEvent =
            new MembershipExpiredDomainEvent(this.FirstName, this.Email, this.TerminationDate.GetValueOrDefault(),
                this.TerminationReason);
        this.RaiseDomainEvent(domainEvent);
    }

    public void Reactivate()
    {
        if (this.Status != MembershipStatus.Expelled && this.Status != MembershipStatus.Suspended &&
            this.Status != MembershipStatus.Expired)
        {
            throw new AggregateInvalidStateException();
        }

        this._suspension = null;
        this._expel = null;
        this.TerminationDate = null;
        this.TerminationReason = null;
        this.Status = MembershipStatus.Active;

        var change = new MemberReactivated();
        this.RaiseChangeEvent(change);
    }

    public void AddGroupMembership(Guid groupId)
    {
        this._groupMemberships.Add(groupId);
        var change = new MemberAddedToGroup(groupId);
        this.RaiseChangeEvent(change);

    }
    public void RemoveGroupMembership(Guid groupId)
    {
        this._groupMemberships.Remove(groupId);
        var change = new MemberRemovedFromGroup(groupId);
        this.RaiseChangeEvent(change);
    }
}