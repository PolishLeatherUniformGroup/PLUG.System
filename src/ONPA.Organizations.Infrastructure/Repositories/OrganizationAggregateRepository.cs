using Microsoft.EntityFrameworkCore;
using ONPA.Common.Domain;
using ONPA.Organizations.Domain;
using ONPA.Organizations.Infrastructure.Database;

namespace ONPA.Organizations.Infrastructure.Repositories;

public sealed class OrganizationAggregateRepository : IAggregateRepository<Organization>
{
    private readonly OrganizationsContext _context;

    public OrganizationAggregateRepository(OrganizationsContext context)
    {
        this._context = context;
    }
    
    public IUnitOfWork UnitOfWork => this._context;

    public async Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken = new())
    {
        return await this._context.ReadAggregate<Organization>(id, cancellationToken);
    }

    public async Task<Organization> CreateAsync(Organization aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        var organization = new ReadModel.Organization
        {
            Id = aggregate.AggregateId,
            Name = aggregate.Name,
            CardPrefix = aggregate.CardPrefix,
            TaxId = aggregate.TaxId,
            AccountNumber = aggregate.AccountNumber,
            Address = aggregate.Address,
            ContactEmail = aggregate.ContactEmail,
            Regon = aggregate.Regon,
        };
        var organizationSettings = new ReadModel.OrganizationSettings()
        {
            OrganizationId = aggregate.AggregateId,
            DaysForAppeal = aggregate.Settings.DaysForAppeal,
            FeePaymentMonth = aggregate.Settings.FeePaymentMonth,
            RequiredRecommendations = aggregate.Settings.RequiredRecommendations,
        };
        this._context.Organizations.Add(organization);
        this._context.OrganizationSettings.Add(organizationSettings);
        return aggregate;
    }

    public async Task<Organization> UpdateAsync(Organization aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        var organization = await this._context.Organizations.FindAsync(new object?[] { aggregate.AggregateId }, cancellationToken: cancellationToken);
        if (organization is not null)
        {
            organization.Name = aggregate.Name;
            organization.CardPrefix = aggregate.CardPrefix;
            organization.TaxId = aggregate.TaxId;
            organization.AccountNumber = aggregate.AccountNumber;
            organization.Address = aggregate.Address;
            organization.ContactEmail = aggregate.ContactEmail;
            organization.Regon = aggregate.Regon;
            this._context.Entry(organization).State = EntityState.Modified;
        }
        var organizationSettings = await this._context.OrganizationSettings.FindAsync(new object?[] { aggregate.AggregateId }, cancellationToken: cancellationToken);
        if (organizationSettings is not null)
        {
            organizationSettings.DaysForAppeal = aggregate.Settings.DaysForAppeal;
            organizationSettings.FeePaymentMonth = aggregate.Settings.FeePaymentMonth;
            organizationSettings.RequiredRecommendations = aggregate.Settings.RequiredRecommendations;
            this._context.Entry(organizationSettings).State = EntityState.Modified;
        }
        
        this.UpdateFees(aggregate);
        return aggregate;
    }
    
    private void UpdateFees(Organization aggregate)
    {
        var organizationFees = this._context.OrganizationFees.Where(r => r.OrganizationId == aggregate.AggregateId);
        foreach (var existingFee in aggregate.MembershipFees)
        {
            var fee = organizationFees.SingleOrDefault(r => r.Year == existingFee.Year);
            if (fee is null)
            {
               var newFee = new ReadModel.OrganizationFee()
               {
                   Amount = existingFee.YearlyAmount.Amount,
                   Currency = existingFee.YearlyAmount.Currency,
                     OrganizationId = aggregate.AggregateId,
                     Year = existingFee.Year,
               };
               this._context.OrganizationFees.Add(newFee);
            }
            
            else
            {
                fee.Amount = existingFee.YearlyAmount.Amount;
                fee.Currency = existingFee.YearlyAmount.Currency;
                this._context.Entry(fee).State = EntityState.Modified;
            }
        }
    }
}