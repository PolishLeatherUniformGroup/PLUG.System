using PLUG.System.Apply.StateEvents;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.Domain;

public partial class ApplicationForm
{
    public void ApplyChange(ApplicationFormCreated change)
    {
        this.TenantId = change.TenantId;
        this.FirstName = change.FirstName;
        this.LastName = change.LastName;
        this.Email = change.Email;
        this.Phone = change.Phone;
        this.RecommendationIds = change.Recommendations;
        this.Address = change.Address;
        this.Status = ApplicationStatus.Received;
        this.ApplicationDate = change.ApplicationDate;
    }

    public void ApplyChange(ApplicationFormCancelled change)
    {
        this.IsValid = false;
        this.Status = ApplicationStatus.Invalid;
        this.ValidationProblem = change.Reason;
    }

    public void ApplyChange(ApplicationFormValidated change)
    {
        this.IsValid = true;
        this.Status = ApplicationStatus.Validated;
        this.RequiredFee = change.RequiredFee;
    }

    public void ApplyChange(ApplicationRecommendationRequested change)
    {
        var recommendation = new Recommendation(change.RecommendationId, change.RecommendingMemberId,
            change.RecommendingMemberNumber,change.RequestedDate);
        this._recommendations.Add(recommendation);
    }

    public void ApplyChange(ApplicationRecommendationEndorsed change)
    {
        var recommendation = this._recommendations.Single(r => r.Id == change.RecommendationId);
        recommendation.EndorseRecommendation();
        if (this._recommendations.All(r => r.IsEndorsed))
        {
            this.Status = ApplicationStatus.AwaitDecision;
        }
    }

    public void ApplyChange(ApplicationRecommendationRefused change)
    {
        var recommendation = this._recommendations.Single(r => r.Id == change.RecommendationId);
        recommendation.RefuseRecommendation();
        this.Status = ApplicationStatus.NotRecomended;
    }
    
    public void ApplyChange(ApplicationFeePaymentRegistered change)
    {
        this.PaidFee ??= new Money(0, this.RequiredFee!.Currency);
        this.PaidFee += change.PaidFee;
        this.FeePaidDate = change.PaidDate;
    }
    
    public void ApplyChange(ApplicationApproved change)
    {
        this.Status = ApplicationStatus.Accepted;
        this.ApproveDate = change.ApproveDate;
    }
    
    public void ApplyChange(ApplicationRejected change)
    {
        this.Status = ApplicationStatus.Rejected;
        this.RejectDate = change.RejectDate;
        this.RejectionDetails = change.DecisionDetails;
        this.AppealDeadline = change.AppealDeadline;
    }
    
    public void ApplyChange(ApplicationRejectionAppealReceived change)
    {
        this.Status = ApplicationStatus.RejectionAppealed;
        this.AppealDate = change.AppealDate;
        this.AppealJustification = change.Justification;
    }
    

    public void ApplyChange(ApplicationRejectionAppealDismissed change)
    {
        this.Status = ApplicationStatus.AppealRejected;
        this.AppealRejectDate = change.RejectDate;
        this.AppealRejectionDetails = change.DecisionDetails;
    }
    
    public void ApplyChange(ApplicationRejectionAppealApproved change)
    {
        this.Status = ApplicationStatus.AppealSuccessful;
        this.AppealApproveDate = change.ApproveDate;
    }
}