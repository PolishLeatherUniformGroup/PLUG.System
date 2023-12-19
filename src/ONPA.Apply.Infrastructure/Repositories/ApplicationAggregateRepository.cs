using Microsoft.EntityFrameworkCore;
using ONPA.Apply.Infrastructure.Database;
using PLUG.System.Apply.Domain;
using Recommendation = ONPA.Apply.Infrastructure.ReadModel.Recommendation;
using ONPA.Common.Infrastructure.Repositories;

namespace ONPA.Apply.Infrastructure.Repositories;

public sealed class ApplicationAggregateRepository : MultiTenantAggregateRootRepository<ApplyContext,ApplicationForm>
{
    private readonly ApplyContext _context;

    public ApplicationAggregateRepository(ApplyContext context) : base(context)
    {
    }

    protected override async Task OnAggregateCreate(ApplicationForm aggregate)
    {
        var model=CreateApplicationForm(aggregate);
        await _context.ApplicationForms.AddAsync(model); 
    }

    protected override async Task OnAggregateUpdate(ApplicationForm aggregate)
    {
        UpdateRecommendations(aggregate);
        await UpdateApplicationForm(aggregate);
    }

    private ReadModel.ApplicationForm CreateApplicationForm(ApplicationForm aggregate)
    {
        return new ReadModel.ApplicationForm()
        {
            Id = aggregate.AggregateId,
            FirstName = aggregate.FirstName,
            LastName = aggregate.LastName,
            Email = aggregate.Email,
            Phone = aggregate.Phone,
            Address = aggregate.Address,
            ApplicationDate = aggregate.ApplicationDate,
            Status = aggregate.Status.Value,
            LastUpdateDate = this.CalculateLastUpdate(aggregate),
            RequiredFeeAmount = aggregate.RequiredFee?.Amount,
            FeeCurrency = aggregate.RequiredFee?.Currency
        };
    }


    private async Task UpdateApplicationForm(ApplicationForm aggregate)
    {
        var applicationForm = await this._context.ApplicationForms.FindAsync(aggregate.AggregateId);
        if (applicationForm is not null)
        {
            applicationForm.LastUpdateDate = this.CalculateLastUpdate(aggregate);
            applicationForm.Status = aggregate.Status.Value;
            applicationForm.RequiredFeeAmount = aggregate.RequiredFee?.Amount;
            applicationForm.FeeCurrency = aggregate.RequiredFee?.Currency;
            applicationForm.PaidFeeAmount = aggregate.PaidFee?.Amount;
            this._context.Entry(applicationForm).State = EntityState.Modified;
        }
    }
    private DateTime CalculateLastUpdate(ApplicationForm aggregate)
    {
        var dates = new List<DateTime?>(8)
        {
            aggregate.ApplicationDate,
            aggregate.AppealDate,
            aggregate.ApproveDate,
            aggregate.AppealApproveDate,
            aggregate.AppealRejectDate,
            aggregate.RejectDate,
            aggregate.FeePaidDate,
        };
        return (DateTime)dates.MaxBy(x => x.GetValueOrDefault())!;
    }
    
    private void UpdateRecommendations(ApplicationForm aggregate)
    {
        var recommendations = this._context.Recommendations.Where(r => r.ApplicationId == aggregate.AggregateId);
        foreach (var existingRecommendation in aggregate.Recommendations)
        {
            var recommendation = recommendations.SingleOrDefault(r => r.Id == existingRecommendation.Id);
            if (recommendation is null)
            {
                var newRecommendation = new Recommendation()
                {
                    Id = existingRecommendation.Id,
                    RecommendingMemberId = existingRecommendation.MemberId,
                    ApplicationId = aggregate.AggregateId,
                    RecommendingMemberNumber = existingRecommendation.MemberNumber,
                    RequestDate = existingRecommendation.RequestedDate,
                    Status = existingRecommendation.IsEndorsed ? 1 : existingRecommendation.IsRefused ? 2 : 0
                };
                this._context.Recommendations.Add(newRecommendation);
            }
            else
            {
                recommendation.Status = existingRecommendation.IsEndorsed ? 1 : existingRecommendation.IsRefused ? 2 : 0;
                this._context.Entry(recommendation).State = EntityState.Modified;
            }
        }
    }
}