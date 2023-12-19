using AutoFixture;
using FluentAssertions;
using NSubstitute;
using ONPA.Common.Domain;
using ONPA.Organizations.Api.Application.CommandHandlers;
using ONPA.Organizations.Api.Application.Commands;
using ONPA.Organizations.Domain;

namespace ONPA.UnitTests.Application.CommandHandlers;

public class OrganizationCommandHandlersTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IAggregateRepository<Organization> _organizationRepository;
    private readonly Organization aggregate;
    
    public OrganizationCommandHandlersTests()
    {
        
        this._organizationRepository = Substitute.For<IAggregateRepository<Organization>>();
        
        var organizationName = this._fixture.Create<string>();
        var cardPrefix = this._fixture.Create<string>();
        var taxId = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var contactEmail = this._fixture.Create<string>();
        var regon = this._fixture.Create<string>();
        var accountNumber = this._fixture.Create<string>();
        
        this.aggregate = new Organization(
            organizationName,
            cardPrefix,
            taxId,
            accountNumber,
            address,
            contactEmail,
            regon);
        this._organizationRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(this.aggregate);
    }

    [Fact]
    public async Task CreateOrganizationCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<CreateOrganizationCommand>();
        var sut = new CreateOrganizationCommandHandler(this._organizationRepository);

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().NotBe(Guid.Empty);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateOrganizationCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ChangeOrganizationDataCommand>();
        var sut = new ChangeOrganizationDataCommandHandler(this._organizationRepository);

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().NotBe(Guid.Empty);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateOrganizationSettingsCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<UpdateOrganizationSettingsCommand>();
        var sut = new UpdateOrganizationSettingsCommandHandler(this._organizationRepository);

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().NotBe(Guid.Empty);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task RequestMembershipFeeCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<RequestMembershipFeeCommand>();
        var sut = new RequestMembershipFeeCommandHandler(this._organizationRepository);

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().NotBe(Guid.Empty);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
}