using AutoFixture;
using FluentAssertions;
using NSubstitute;
using ONPA.Apply.Api.Application.CommandHandlers;
using ONPA.Apply.Api.Application.Commands;
using ONPA.Common.Domain;
using PLUG.System.Apply.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.UnitTests.Application.CommandHandlers;

public class ApplicationFormCommandHandlersTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IAggregateRepository<ApplicationForm> _aggregateRepository;
    private readonly ApplicationForm _aggregate;
    
    private readonly Guid _tenantId = Guid.NewGuid();
    
    
    public ApplicationFormCommandHandlersTests()
    {
        this._aggregateRepository = Substitute.For<IAggregateRepository<ApplicationForm>>();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(2).ToList();

        // Act
        this._aggregate = new ApplicationForm(this._tenantId,firstName, lastName, email, phone, recommendations, address);

        
        this._aggregateRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(this._aggregate);
    }

    [Fact]
    public async Task CreateApplicationFormCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<CreateApplicationFormCommand>();
        var handler = new CreateApplicationFormCommandHandler(this._aggregateRepository);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task ValidateApplicationFormCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ValidateApplicationFormCommand>();
        var handler = new ValidateApplicationFormCommandHandler(this._aggregateRepository);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task EndorseApplicationRecommendationCommandShouldBeHandled()
    {
        // Arrange
        
        var handler = new EndorseApplicationRecommendationCommandHandler(this._aggregateRepository);
        
        this._aggregate.AcceptApplicationForm(new Money(100));
        var recommendingMemberId = this._fixture.Create<Guid>();
        var recommendingMemberNumber = this._fixture.Create<string>();
        this._aggregate.RequestRecommendation(recommendingMemberId, recommendingMemberNumber);
        
        var command = this._fixture.Build<EndorseApplicationRecommendationCommand>()
            .With(x => x.RecommendingMemberId, recommendingMemberId)
            .With(x => x.RecommendationId, this._aggregate.Recommendations.First().Id)
            .Create();
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task RefuseApplicationRecommendationCommandShouldBeHandled()
    {
        // Arrange
        
        var handler = new RefuseApplicationRecommendationCommandHandler(this._aggregateRepository);
        
        this._aggregate.AcceptApplicationForm(new Money(100));
        var recommendingMemberId = this._fixture.Create<Guid>();
        var recommendingMemberNumber = this._fixture.Create<string>();
        this._aggregate.RequestRecommendation(recommendingMemberId, recommendingMemberNumber);
        
        var command = this._fixture.Build<RefuseApplicationRecommendationCommand>()
            .With(x => x.ApplicationFormId, this._aggregate.AggregateId)
            .With(x => x.RecommendingMemberId, recommendingMemberId)
            .With(x => x.RecommendationId, this._aggregate.Recommendations.First().Id)
            .Create();
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    [Fact]
    public async Task RegisterApplicationFeePaymentCommandShouldBeHandled()
    {
        // Arrange
        var handler = new RegisterApplicationFeePaymentCommandHandler(this._aggregateRepository);
        this._aggregate.AcceptApplicationForm(new Money(100));
        var recommendingMemberId = this._fixture.Create<Guid>();
        var recommendingMemberNumber = this._fixture.Create<string>();
        this._aggregate.RequestRecommendation(recommendingMemberId, recommendingMemberNumber);
        this._aggregate.EndorseRecommendation(this._aggregate.Recommendations.First().Id,recommendingMemberId);
        var aggregateRequiredFee = this._aggregate.RequiredFee;
        this._aggregate.RegisterFeePayment(aggregateRequiredFee!, this._fixture.Create<DateTime>());
        
        var command = this._fixture.Build<RegisterApplicationFeePaymentCommand>()
            .With(x=>x.Paid,aggregateRequiredFee)
            .Create();
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    
    [Fact]
    public async Task ApproveApplicationCommandShouldBeHandled()
    {
        // Arrange
        var handler = new ApproveApplicationCommandHandler(this._aggregateRepository);
        this._aggregate.AcceptApplicationForm(new Money(100));
        var recommendingMemberId = this._fixture.Create<Guid>();
        var recommendingMemberNumber = this._fixture.Create<string>();
        this._aggregate.RequestRecommendation(recommendingMemberId, recommendingMemberNumber);
        this._aggregate.EndorseRecommendation(this._aggregate.Recommendations.First().Id,recommendingMemberId);
        var aggregateRequiredFee = this._aggregate.RequiredFee;
        this._aggregate.RegisterFeePayment(aggregateRequiredFee!, this._fixture.Create<DateTime>());
        
        var command = this._fixture.Create<ApproveApplicationCommand>();
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task RejectApplicationCommandShouldBeHandled()
    {
        // Arrange
        var handler = new RejectApplicationCommandHandler(this._aggregateRepository);
        this._aggregate.AcceptApplicationForm(new Money(100));
        var recommendingMemberId = this._fixture.Create<Guid>();
        var recommendingMemberNumber = this._fixture.Create<string>();
        this._aggregate.RequestRecommendation(recommendingMemberId, recommendingMemberNumber);
        this._aggregate.EndorseRecommendation(this._aggregate.Recommendations.First().Id,recommendingMemberId);
        var aggregateRequiredFee = this._aggregate.RequiredFee;
        this._aggregate.RegisterFeePayment(aggregateRequiredFee!, this._fixture.Create<DateTime>());
        
        var command = this._fixture.Create<RejectApplicationCommand>();
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task AppealApplicationRejectionCommandShouldBeHandled()
    {
        // Arrange
        var handler = new AppealApplicationRejectionCommandHandler(this._aggregateRepository);
        this._aggregate.AcceptApplicationForm(new Money(100));
        var recommendingMemberId = this._fixture.Create<Guid>();
        var recommendingMemberNumber = this._fixture.Create<string>();
        this._aggregate.RequestRecommendation(recommendingMemberId, recommendingMemberNumber);
        this._aggregate.EndorseRecommendation(this._aggregate.Recommendations.First().Id,recommendingMemberId);
        var aggregateRequiredFee = this._aggregate.RequiredFee;
        this._aggregate.RegisterFeePayment(aggregateRequiredFee!, this._fixture.Create<DateTime>());
        this._aggregate.RejectApplication(this._fixture.Create<DateTime>(), this._fixture.Create<string>(),14);
        
        var command = this._fixture.Create<AppealApplicationRejectionCommand>();
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task ApproveApplicationRejectAppealCommandShouldBeHandled()
    {
        // Arrange
        var handler = new ApproveApplicationRejectionAppealCommandHandler(this._aggregateRepository);
        this._aggregate.AcceptApplicationForm(new Money(100));
        var recommendingMemberId = this._fixture.Create<Guid>();
        var recommendingMemberNumber = this._fixture.Create<string>();
        this._aggregate.RequestRecommendation(recommendingMemberId, recommendingMemberNumber);
        this._aggregate.EndorseRecommendation(this._aggregate.Recommendations.First().Id,recommendingMemberId);
        var aggregateRequiredFee = this._aggregate.RequiredFee;
        this._aggregate.RegisterFeePayment(aggregateRequiredFee!, this._fixture.Create<DateTime>());
        this._aggregate.RejectApplication(this._fixture.Create<DateTime>(), this._fixture.Create<string>(),14);
        this._aggregate.AppealRejection(DateTime.UtcNow.AddDays(1), this._fixture.Create<string>());
        
        var command = this._fixture.Create<ApproveApplicationRejectionAppealCommand>();
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task DismissApplicationRejectAppealCommandShouldBeHandled()
    {
        // Arrange
        var handler = new DismissApplicationRejectionAppealCommandHandler(this._aggregateRepository);
        this._aggregate.AcceptApplicationForm(new Money(100));
        var recommendingMemberId = this._fixture.Create<Guid>();
        var recommendingMemberNumber = this._fixture.Create<string>();
        this._aggregate.RequestRecommendation(recommendingMemberId, recommendingMemberNumber);
        this._aggregate.EndorseRecommendation(this._aggregate.Recommendations.First().Id,recommendingMemberId);
        var aggregateRequiredFee = this._aggregate.RequiredFee;
        this._aggregate.RegisterFeePayment(aggregateRequiredFee!, this._fixture.Create<DateTime>());
        this._aggregate.RejectApplication(this._fixture.Create<DateTime>(), this._fixture.Create<string>(),14);
        this._aggregate.AppealRejection(DateTime.UtcNow.AddDays(1), this._fixture.Create<string>());
        
        var command = this._fixture.Create<DismissApplicationRejectionAppealCommand>();
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
}