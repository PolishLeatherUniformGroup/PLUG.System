using AutoFixture;
using FluentAssertions;
using ONPA.Common.Domain;
using ONPA.Organizations.Domain;
using ONPA.Organizations.StateEvents;
using PLUG.System.SharedDomain;

namespace ONPA.UnitTests.Domain;

public class OrganizationShould
{
    private readonly IFixture _fixture;
    
    public OrganizationShould()
    {
        this._fixture = new Fixture();
    }
    
    [Fact]
    public void CreateOrganization()
    {
        // Arrange
        var organizationName = this._fixture.Create<string>();
        var cardPrefix = this._fixture.Create<string>();
        var taxId = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var contactEmail = this._fixture.Create<string>();
        var regon = this._fixture.Create<string>();
        var accountNumber = this._fixture.Create<string>();
        
        // Act
        var aggregate = new Organization(
            organizationName,
            cardPrefix,
            taxId,
            accountNumber,
            address,
            contactEmail,
            regon);
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBe(Guid.Empty);
        aggregate.Name.Should().Be(organizationName);
        aggregate.CardPrefix.Should().Be(cardPrefix);
        aggregate.TaxId.Should().Be(taxId);
        aggregate.Address.Should().Be(address);
        aggregate.ContactEmail.Should().Be(contactEmail);
        aggregate.Regon.Should().Be(regon);
        aggregate.AccountNumber.Should().Be(accountNumber);

        aggregate.GetStateEvents().Should().HaveCount(1);
        aggregate.GetStateEvents().First().Should().BeOfType<OrganizationCreated>();
    }
    
    [Fact]
    public void UpdateOrganization()
    {
        // Arrange
        var organizationName = this._fixture.Create<string>();
        var cardPrefix = this._fixture.Create<string>();
        var taxId = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var contactEmail = this._fixture.Create<string>();
        var regon = this._fixture.Create<string>();
        var accountNumber = this._fixture.Create<string>();
        
        var aggregate = new Organization(
            organizationName,
            cardPrefix,
            taxId,
            accountNumber,
            address,
            contactEmail,
            regon);
        
        var newOrganizationName = this._fixture.Create<string>();
        var newCardPrefix = this._fixture.Create<string>();
        var newTaxId = this._fixture.Create<string>();
        var newAddress = this._fixture.Create<string>();
        var newContactEmail = this._fixture.Create<string>();
        var newRegon = this._fixture.Create<string>();
        var newAccountNumber = this._fixture.Create<string>();
        
        // Act
        aggregate.UpdateOrganizationData(newOrganizationName,
            newCardPrefix,
            newTaxId,
            newAccountNumber,
            newAddress,
            newContactEmail,
            newRegon);
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBe(Guid.Empty);
        aggregate.Name.Should().Be(newOrganizationName);
        aggregate.CardPrefix.Should().Be(newCardPrefix);
        aggregate.TaxId.Should().Be(newTaxId);
        aggregate.Address.Should().Be(newAddress);
        aggregate.ContactEmail.Should().Be(newContactEmail);
        aggregate.Regon.Should().Be(newRegon);
        aggregate.AccountNumber.Should().Be(newAccountNumber);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Last().Should().BeOfType<OrganizationDataUpdated>();
    }
    
     [Fact]
    public void UpdateOrganizationSettings()
    {
        // Arrange
        var organizationName = this._fixture.Create<string>();
        var cardPrefix = this._fixture.Create<string>();
        var taxId = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var contactEmail = this._fixture.Create<string>();
        var regon = this._fixture.Create<string>();
        var accountNumber = this._fixture.Create<string>();
        
        var aggregate = new Organization(
            organizationName,
            cardPrefix,
            taxId,
            accountNumber,
            address,
            contactEmail,
            regon);
        
       var settings = this._fixture.Create<OrganizationSettings>();
        
        // Act
        aggregate.UpdateSettings(settings);
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBe(Guid.Empty);
        aggregate.Settings.Should().BeEquivalentTo(settings);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Last().Should().BeOfType<OrganizationSettingsUpdated>();
    }

    [Fact]
    public void RequestMembershipFee()
    {
        // Arrange
        var organizationName = this._fixture.Create<string>();
        var cardPrefix = this._fixture.Create<string>();
        var taxId = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var contactEmail = this._fixture.Create<string>();
        var regon = this._fixture.Create<string>();
        var accountNumber = this._fixture.Create<string>();
        
        var aggregate = new Organization(
            organizationName,
            cardPrefix,
            taxId,
            accountNumber,
            address,
            contactEmail,
            regon);
        
        var fee = this._fixture.Create<decimal>();
        var currency = this._fixture.Create<string>();
        var year = DateTime.Now.Year;
        
        // Act
        aggregate.RequestMembershipFee(year, new Money(fee, currency));
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBe(Guid.Empty);
        aggregate.MembershipFees.Should().HaveCount(1);
        var membershipFee = aggregate.MembershipFees.First();
        membershipFee.Year.Should().Be(year);
        membershipFee.YearlyAmount.Amount.Should().Be(fee);
        membershipFee.YearlyAmount.Currency.Should().Be(currency);
        
        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MembershipFeeRequested>();
    }

    [Fact]
    public void Recreated()
    {
        // Arrange
        var organizationName = this._fixture.Create<string>();
        var cardPrefix = this._fixture.Create<string>();
        var taxId = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var contactEmail = this._fixture.Create<string>();
        var regon = this._fixture.Create<string>();
        var accountNumber = this._fixture.Create<string>();
        
        var aggregate = new Organization(
            organizationName,
            cardPrefix,
            taxId,
            accountNumber,
            address,
            contactEmail,
            regon);
        
        var newOrganizationName = this._fixture.Create<string>();
        var newCardPrefix = this._fixture.Create<string>();
        var newTaxId = this._fixture.Create<string>();
        var newAddress = this._fixture.Create<string>();
        var newContactEmail = this._fixture.Create<string>();
        var newRegon = this._fixture.Create<string>();
        var newAccountNumber = this._fixture.Create<string>();

        aggregate.UpdateOrganizationData(newOrganizationName,
            newCardPrefix,
            newTaxId,
            newAccountNumber,
            newAddress,
            newContactEmail,
            newRegon);   
        var fee = this._fixture.Create<decimal>();
        var currency = this._fixture.Create<string>();
        var year = DateTime.Now.Year;
        aggregate.RequestMembershipFee(year, new Money(fee, currency));
        var settings = this._fixture.Create<OrganizationSettings>();
        aggregate.UpdateSettings(settings);
        
      var events = new List<IStateEvent>();
        
        events.AddRange(aggregate.GetStateEvents());
        aggregate.ClearChanges();
        aggregate.ClearDomainEvents();
        
        // Act
        var newAggregate = new Organization(aggregate.AggregateId,events);
        
        // Assert
        newAggregate.Should().NotBeNull();
        newAggregate.AggregateId.Should().NotBe(Guid.Empty);
        newAggregate.Should().BeEquivalentTo(aggregate);

    }
    
}