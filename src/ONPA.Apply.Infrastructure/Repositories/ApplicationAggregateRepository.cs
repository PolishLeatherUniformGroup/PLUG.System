using MediatR;
using Microsoft.EntityFrameworkCore;
using ONPA.Apply.Infrastructure.Database;
using PLUG.System.Apply.Domain;
using ONPA.Common.Domain;
using RecommendationRead = ONPA.Apply.Infrastructure.ReadModel.Recommendation;
using ApplicationFormRead = ONPA.Apply.Infrastructure.ReadModel.ApplicationForm;
using Recommendation = ONPA.Apply.Infrastructure.ReadModel.Recommendation;

namespace ONPA.Apply.Infrastructure.Repositories;

public sealed class ApplicationAggregateRepository : IAggregateRepository<ApplicationForm>
{
    private readonly ApplyContext _context;


    public ApplicationAggregateRepository(ApplyContext context, IMediator mediator)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => this._context;

    public async Task<ApplicationForm?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = new())
    {
        return await this._context.ReadAggregate<ApplicationForm>(id, cancellationToken);
    }

    public async Task<ApplicationForm> CreateAsync(ApplicationForm aggregate,
        CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        var applicationForm = new ReadModel.ApplicationForm()
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
        this._context.ApplicationForms.Add(applicationForm);
        this.UpdateRecommendations(aggregate);

        return aggregate;
    }

    public async Task<ApplicationForm> UpdateAsync(ApplicationForm aggregate,
        CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        this.UpdateRecommendations(aggregate);
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
        return aggregate;
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
            aggregate.DecisionExpectDate,
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