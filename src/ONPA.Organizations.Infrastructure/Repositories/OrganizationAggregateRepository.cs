using Microsoft.EntityFrameworkCore;
using ONPA.Common.Domain;
using ONPA.Common.Infrastructure.Repositories;
using ONPA.Organizations.Domain;
using ONPA.Organizations.Infrastructure.Database;
using System.Threading;

namespace ONPA.Organizations.Infrastructure.Repositories;

public sealed class OrganizationAggregateRepository : AggregateRootRepository<OrganizationsContext, Organization>
{
  

    public OrganizationAggregateRepository(OrganizationsContext context) : base(context) { }

    protected override async Task OnAggregateCreate(Organization aggregate)
    {
        await CreateModelAsync(aggregate);
    }

    protected override async Task OnAggregateUpdate(Organization aggregate)
    {
        await UpdateOrganizationModel(aggregate);
        UpdateFees(aggregate);
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

    private async Task CreateModelAsync(Organization aggregate)
    {
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
       await this._context.Organizations.AddAsync(organization);
        await this._context.OrganizationSettings.AddAsync(organizationSettings);
    }

    private async Task UpdateOrganizationModel(Organization aggregate)
    {
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
    }
}