using PLUG.System.Apply.DomainEvents;
using PLUG.System.Apply.StateEvents;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.Domain;

public sealed partial class ApplicationForm : AggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public List<string> RecommendationIds { get; private set; }
    public string Address { get; private set; }
    public DateTime ApplicationDate { get; private set; }
    public DateTime? ApproveDate { get; private set; }
    public DateTime? RejectDate { get; private set; }
    public DateTime? AppealDeadline { get; private set; }
    public DateTime? AppealDate { get; private set; }
    public DateTime? AppealApproveDate { get; private set; }
    public DateTime? AppealRejectDate { get; private set; }
    public DateTime? FeePaidDate { get; private set; }
    public DateTime? DecisionExpectDate { get; private set; }

    public ApplicationStatus Status { get; private set; }
    public bool? IsValid { get; private set; }
    public string? ValidationProblem { get; private set; }

    private readonly List<Recommendation> _recommendations = new();
    public IEnumerable<Recommendation> Recommendations => this._recommendations;

    public Money? RequiredFee { get; private set; }
    public Money? PaidFee { get; private set; }

    public bool IsPaid => this.RequiredFee == this.PaidFee;

    public string? RejectionDetails { get; private set; }
    public string? AppealJustification { get; private set; }
    public string? AppealRejectionDetails { get; private set; }

    public ApplicationForm(Guid aggregateId, IEnumerable<IStateEvent> changes) : base(aggregateId, changes)
    {
    }

    public ApplicationForm(string firstName, string lastName, string email, string phone, List<string> recommendations,
        string address)
    {
        this.FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        this.LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        this.Email = email ?? throw new ArgumentNullException(nameof(email));
        this.Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        this.RecommendationIds = recommendations ?? throw new ArgumentNullException(nameof(recommendations));
        this.Address = address ?? throw new ArgumentNullException(nameof(address));
        this.ApplicationDate = DateTime.UtcNow;

        var change =
            new ApplicationFormCreated(firstName, lastName, email, phone,recommendations, address, this.ApplicationDate);
        this.Status = ApplicationStatus.Received;
        this.RaiseChangeEvent(change);

        var domainEvent = new ApplicationReceivedDomainEvent(firstName, lastName, email, recommendations);
        this.RaiseDomainEvent(domainEvent);
    }

    /// <summary>
    /// Accept Application Form occurs when it meet validation criteria.
    /// </summary>
    public void AcceptApplicationForm(Money requiredFee)
    {
        if (this.Status != ApplicationStatus.Received)
        {
            throw new AggregateInvalidStateException();
        }

        this.IsValid = true;
        this.Status = ApplicationStatus.Validated;
        this.RequiredFee = requiredFee;

        var change = new ApplicationFormValidated(requiredFee);
        this.RaiseChangeEvent(change);

        var domainEvent = new ApplicationValidatedDomainEvent(requiredFee, this.FirstName, this.Email);
        this.RaiseDomainEvent(domainEvent);
    }

    /// <summary>
    ///  Cancel Application Form occurs when it does not meet validation criteria.
    /// </summary>
    /// <param name="problem">Reason of failed validation</param>
    public void CancelApplicationForm(string problem)
    {
        if (this.Status != ApplicationStatus.Received)
        {
            throw new AggregateInvalidStateException();
        }

        this.IsValid = false;
        this.ValidationProblem = problem;
        this.Status = ApplicationStatus.Invalid;

        var change = new ApplicationFormCancelled(problem);
        this.RaiseChangeEvent(change);

        var domainEvent = new ApplicationCancelledDomainEvent(problem, this.FirstName, this.Email);
        this.RaiseDomainEvent(domainEvent);
    }

    /// <summary>
    /// Creates recommendation request later send to recommending members.
    /// </summary>
    /// <param name="recommendingMemberId"></param>
    /// <param name="recommendingMemberNumber"></param>
    public void RequestRecommendation(Guid recommendingMemberId, string recommendingMemberNumber)
    {
        if (this.Status != ApplicationStatus.Validated)
        {
            throw new AggregateInvalidStateException();
        }

        var recommendation = new Recommendation(recommendingMemberId, recommendingMemberNumber, DateTime.UtcNow);
        this._recommendations.Add(recommendation);

        var change = new ApplicationRecommendationRequested(recommendation.Id, recommendingMemberId,
            recommendingMemberNumber, recommendation.RequestedDate);
        this.RaiseChangeEvent(change);
        
        var domainEvent = new ApplicationRecommendationRequestedDomainEvent(recommendingMemberId,FirstName,LastName);
        this.RaiseDomainEvent(domainEvent);
    }

    public void EndorseRecommendation(Guid recommendationId, Guid recommendingMemberId)
    {
        if (this.Status != ApplicationStatus.Validated)
        {
            throw new AggregateInvalidStateException();
        }

        var recommendation =
            this._recommendations.SingleOrDefault(r => r.Id == recommendationId && r.MemberId == recommendingMemberId);
        if (recommendation == null)
        {
            throw new EntityNotFoundException();
        }

        recommendation.EndorseRecommendation();

        var change = new ApplicationRecommendationEndorsed(recommendation.Id);
        this.RaiseChangeEvent(change);

        if (this._recommendations.All(r => r.IsEndorsed))
        {
            this.Status = ApplicationStatus.AwaitDecision;

            var domainEvent = new ApplicationRecommendedDomainEvent(this.FirstName, this.LastName);
            this.RaiseDomainEvent(domainEvent);
        }
    }

    /// <summary>
    /// Refuse recommendation by member
    /// </summary>
    /// <param name="recommendationId"></param>
    /// <param name="recommendingMemberId"></param>
    public void RefuseRecommendation(Guid recommendationId, Guid recommendingMemberId)
    {
        if (this.Status != ApplicationStatus.Validated)
        {
            throw new AggregateInvalidStateException();
        }

        var recommendation =
            this._recommendations.SingleOrDefault(r => r.Id == recommendationId && r.MemberId == recommendingMemberId);
        if (recommendation == null)
        {
            throw new EntityNotFoundException();
        }

        recommendation.RefuseRecommendation();
        this.Status = ApplicationStatus.NotRecomended;

        var change = new ApplicationRecommendationRefused(recommendation.Id);
        this.RaiseChangeEvent(change);

        var domainEvent = new ApplicationNotRecommendedDomainEvent(this.FirstName, this.Email);
        this.RaiseDomainEvent(domainEvent);
    }

    public void RegisterFeePayment(Money paidFee, DateTime paidDate, int daysToDecision)
    {
        if (this.Status != ApplicationStatus.AwaitDecision && this.Status != ApplicationStatus.Validated)
        {
            throw new AggregateInvalidStateException();
        }

        if (this.RequiredFee is null)
        {
            return;
        }

        this.PaidFee ??= new Money(0, this.RequiredFee.Currency);
        this.PaidFee += paidFee;
        this.FeePaidDate = paidDate;
       

        var change = new ApplicationFeePaymentRegistered(paidFee,paidDate,this.DecisionExpectDate);
        this.RaiseChangeEvent(change);

        if (this.PaidFee < this.RequiredFee)
        {
            var domainEvent =
                new ApplicationFeeNotBalancedDomainEvent(this.FirstName, this.Email, this.RequiredFee, this.PaidFee);
            this.RaiseDomainEvent(domainEvent);
        }
        else
        {
            this.DecisionExpectDate = paidDate.AddDays(daysToDecision);
            var domainEvent = new ApplicationFeeBalancedDomainEvent(this.FirstName, this.Email, this.DecisionExpectDate.Value);
            this.RaiseDomainEvent(domainEvent);
        }
    }

    public void ApproveApplication()
    {
        if (this.Status != ApplicationStatus.AwaitDecision || !this.IsPaid)
        {
            throw new AggregateInvalidStateException();
        }

        this.ApproveDate = DateTime.UtcNow;
        this.Status = ApplicationStatus.Accepted;

        var change = new ApplicationApproved(this.ApproveDate.Value);
        this.RaiseChangeEvent(change);

        var domainEvent = new ApplicationApprovedDomainEvent(this.FirstName,this.LastName,this.Email,this.Phone,
            this.Address,this.ApplicationDate,this.ApproveDate.Value,this.PaidFee!);
        this.RaiseDomainEvent(domainEvent);
    }
 
    public void RejectApplication(string decisionDetail, int daysToAppeal)
    {
        if (this.Status != ApplicationStatus.AwaitDecision)
        {
            throw new AggregateInvalidStateException();
        }

        this.RejectionDetails = decisionDetail;
        this.RejectDate = DateTime.UtcNow;
        this.Status = ApplicationStatus.Rejected;
        this.AppealDeadline = this.RejectDate.Value.AddDays(daysToAppeal);

        var change = new ApplicationRejected(this.RejectDate.Value, decisionDetail, this.AppealDeadline.Value);
        this.RaiseChangeEvent(change);

        var domainEvent =
            new ApplicationRejectedDomainEvent(this.FirstName,this.Email, this.RejectDate.Value, decisionDetail, this.AppealDeadline.Value);
        this.RaiseDomainEvent(domainEvent);
    }

    public void AppealRejection(DateTime receivedDate, string justification)
    {
        if (this.Status != ApplicationStatus.Rejected)
        {
            throw new AggregateInvalidStateException();
        }

        this.AppealDate = receivedDate;
        this.Status = ApplicationStatus.RejectionAppealed;
        this.AppealJustification = justification;

        var change = new ApplicationRejectionAppealReceived(this.AppealDate.Value, justification);
        this.RaiseChangeEvent(change);

        if (receivedDate.Date > this.AppealDeadline.GetValueOrDefault().Date)
        {
            this.Status = ApplicationStatus.AppealRejected;
            this.AppealRejectDate = DateTime.UtcNow;
            this.AppealRejectionDetails = "Odwołanie wpłynęło po terminie.";

            var autoDecision = new ApplicationRejectionAppealDismissed(this.AppealRejectDate.Value, this.AppealRejectionDetails);
            this.RaiseChangeEvent(autoDecision);

            var rejectionEvent = new ApplicationRejectionAppealDismissedDomainEvent(this.FirstName,this.Email, this.AppealRejectDate.Value, this.AppealRejectionDetails);
            this.RaiseDomainEvent(rejectionEvent);
            return;
        }

        var domainEvent = new ApplicationRejectionAppealReceivedDomainEvent(this.FirstName,this.Email,this.AppealDate.Value);
        this.RaiseDomainEvent(domainEvent);
    }

    public void ApproveAppeal(DateTime approveDate)
    {
        if (this.Status != ApplicationStatus.RejectionAppealed)
        {
            throw new AggregateInvalidStateException();
        }

        this.Status = ApplicationStatus.AppealSuccessful;
        this.AppealApproveDate = approveDate;

        var change = new ApplicationRejectionAppealApproved(approveDate);
        this.RaiseChangeEvent(change);

        var domainEvent = new ApplicationRejectionAppealApprovedDomainEvent(this.FirstName,this.LastName,this.Email,
            this.Phone,this.Address,this.ApplicationDate,this.AppealApproveDate.Value, this.PaidFee!);
        this.RaiseDomainEvent(domainEvent);
    }

    public void DismissAppeal(DateTime rejectDate, string decisionDetail)
    {
        if (this.Status != ApplicationStatus.RejectionAppealed)
        {
            throw new AggregateInvalidStateException();
        }

        this.Status = ApplicationStatus.AppealRejected;
        this.AppealRejectDate = rejectDate;
        this.AppealRejectionDetails = decisionDetail;

        var change = new ApplicationRejectionAppealDismissed(rejectDate, decisionDetail);
        this.RaiseChangeEvent(change);

        var domainEvent = new ApplicationRejectionAppealDismissedDomainEvent(this.FirstName,this.Email, rejectDate, decisionDetail);
        this.RaiseDomainEvent(domainEvent);
    }
}