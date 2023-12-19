﻿using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.StateEvents;

public sealed class EnrollmentPaymentRegistered : StateEventBase
{
    public Guid EnrollmentId { get; private set; }
    public DateTime PaidDate { get; private set; }
    public Money PaidAmount { get; private set; }

    public EnrollmentPaymentRegistered(Guid enrollmentId, DateTime paidDate, Money paidAmount)
    {
        this.EnrollmentId = enrollmentId;
        this.PaidDate = paidDate;
        this.PaidAmount = paidAmount;
    }
        
    private EnrollmentPaymentRegistered(Guid tenantId, Guid aggregateId, long aggregateVersion, Guid enrollmentId, DateTime paidDate, Money paidAmount) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.EnrollmentId = enrollmentId;
        this.PaidDate = paidDate;
        this.PaidAmount = paidAmount;
    }
    
    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new EnrollmentPaymentRegistered(tenantId, aggregateId, aggregateVersion, this.EnrollmentId, this.PaidDate, this.PaidAmount);
    }
        
}