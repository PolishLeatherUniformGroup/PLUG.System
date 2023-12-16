using AutoFixture;
using FluentAssertions;
using PLUG.System.Apply.Domain;
using PLUG.System.Apply.DomainEvents;
using PLUG.System.Apply.StateEvents;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using PLUG.System.SharedDomain;

namespace ONPA.UnitTests.Domain;

public class ApplicationFormShould
{
    private IFixture _fixture;
    private readonly Guid tenantId = Guid.NewGuid();

    public ApplicationFormShould()
    {
        this._fixture = new Fixture();
    }

    [Fact]
    public void EmmitEvents_WhenCreated()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(2).ToList();

        // Act
        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().First().Should().BeOfType<ApplicationReceivedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(1);
        aggregate.GetStateEvents().First().Should().BeOfType<ApplicationFormCreated>();
        aggregate.FirstName.Should().Be(firstName);
        aggregate.LastName.Should().Be(lastName);
        aggregate.Email.Should().Be(email);
        aggregate.Phone.Should().Be(phone);
        aggregate.Address.Should().Be(address);
        aggregate.Status.Should().Be(ApplicationStatus.Received);
        aggregate.Recommendations.Should().BeEmpty();
        aggregate.ApplicationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Fact]
    public void SetFeeAndEmmitEvents_WhenAccepted()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(2).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);
        //Act

        aggregate.AcceptApplicationForm(requiredFee);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationValidatedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationFormValidated>();
        aggregate.Status.Should().Be(ApplicationStatus.Validated);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().Be(requiredFee);
    }

    [Fact]
    public void ThrowOnAccept_WhenAlreadyAccepted()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(2).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);
        aggregate.AcceptApplicationForm(requiredFee);

        //Act
        // Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.AcceptApplicationForm(requiredFee));
    }

    [Fact]
    public void HaveInvalidStatusAndEmmitEvents_WhenCancelled()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(2).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);
        var problem = this._fixture.Create<string>();
        //Act

        aggregate.CancelApplicationForm(problem);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationCancelledDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationFormCancelled>();
        aggregate.Status.Should().Be(ApplicationStatus.Invalid);
        aggregate.IsValid.Should().BeFalse();
        aggregate.RequiredFee.Should().BeNull();
    }

    [Fact]
    public void ThrowOnCancel_WhenAlreadyAccepted()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(2).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);
        aggregate.AcceptApplicationForm(requiredFee);

        var problem = this._fixture.Create<string>();

        //Act
        // Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.CancelApplicationForm(problem));
    }

    [Fact]
    public void HaveRecommendationsAndEmmitEvents_WhenRecommendationRequested()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        //Act

        aggregate.RequestRecommendation(memberId, memberNumber);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(3);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationRecommendationRequestedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(3);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationRecommendationRequested>();
        aggregate.Status.Should().Be(ApplicationStatus.Validated);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);
        var recommendation = aggregate.Recommendations.First();
        recommendation.Id.Should().NotBeEmpty();
        recommendation.MemberId.Should().Be(memberId);
        recommendation.MemberNumber.Should().Be(memberNumber);
        recommendation.RequestedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        recommendation.IsEndorsed.Should().BeFalse();
        recommendation.IsRefused.Should().BeFalse();
    }

    [Fact]
    public void ThrowOnRequestRecommendation_WhenNotValidated()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(2).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();

        //Act
        // Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.RequestRecommendation(memberId, memberNumber));
    }

    [Fact]
    public void EmmitOneEvent_WhenOneRecommendationEndorsed()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);
        var secondMemberId = this._fixture.Create<Guid>();
        var secondMemberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(secondMemberId, secondMemberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        //Act

        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().NotContainItemsAssignableTo<ApplicationRecommendedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(5);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationRecommendationEndorsed>();
        aggregate.Status.Should().Be(ApplicationStatus.Validated);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(2);

        recommendation.Id.Should().NotBeEmpty();
        recommendation.MemberId.Should().Be(memberId);
        recommendation.MemberNumber.Should().Be(memberNumber);
        recommendation.RequestedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        recommendation.IsEndorsed.Should().BeTrue();
        recommendation.IsRefused.Should().BeFalse();
    }

    [Fact]
    public void ThrowOnEndorseRecommendation_WhenRecommendationNotExists()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();

        //Act
        //Assert
        Assert.Throws<EntityNotFoundException>(() => aggregate.EndorseRecommendation(Guid.NewGuid(), memberId));
    }

    [Fact]
    public void ThrowOnEndorseRecommendation_WhenNotValidated()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        var memberId = this._fixture.Create<Guid>();

        //Act
        //Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.EndorseRecommendation(Guid.NewGuid(), memberId));
    }

    [Fact]
    public void ChangeStatusAndEmmitEvents_WhenLastRecommendationEndorsed()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        //Act

        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationRecommendedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationRecommendationEndorsed>();
        aggregate.Status.Should().Be(ApplicationStatus.AwaitDecision);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);

        recommendation.Id.Should().NotBeEmpty();
        recommendation.MemberId.Should().Be(memberId);
        recommendation.MemberNumber.Should().Be(memberNumber);
        recommendation.RequestedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        recommendation.IsEndorsed.Should().BeTrue();
        recommendation.IsRefused.Should().BeFalse();
    }

    [Fact]
    public void ChangeStatusAndEmmitEvents_WhenFirstRecommendationRefused()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        //Act

        aggregate.RefuseRecommendation(recommendation.Id, memberId);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationNotRecommendedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationRecommendationRefused>();
        aggregate.Status.Should().Be(ApplicationStatus.NotRecomended);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);

        recommendation.Id.Should().NotBeEmpty();
        recommendation.MemberId.Should().Be(memberId);
        recommendation.MemberNumber.Should().Be(memberNumber);
        recommendation.RequestedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        recommendation.IsEndorsed.Should().BeFalse();
        recommendation.IsRefused.Should().BeTrue();
    }

    [Fact]
    public void ThrowOnRefuseRecommendation_WhenRecommendationNotExists()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();

        //Act
        //Assert
        Assert.Throws<EntityNotFoundException>(() => aggregate.RefuseRecommendation(Guid.NewGuid(), memberId));
    }

    [Fact]
    public void ThrowOnRefuseRecommendation_WhenNotValidated()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        var memberId = this._fixture.Create<Guid>();

        //Act
        //Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.RefuseRecommendation(Guid.NewGuid(), memberId));
    }

    [Fact]
    public void BePaidAndEmmitEvents_WhenPaymentBalance()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        //Act

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(5);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationFeeBalancedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(5);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationFeePaymentRegistered>();
        aggregate.Status.Should().Be(ApplicationStatus.AwaitDecision);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);
        aggregate.PaidFee?.Amount.Should().Be(requiredFeeAmount);
        aggregate.IsPaid.Should().BeTrue();
        aggregate
            .DecisionExpectDate.Should().HaveValue()
            .And
            .BeBefore(DateTime.UtcNow.AddDays(15).Date);
    }

    [Fact]
    public void ThrowOnRegisterPayment_WhenNotValidated()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        //Act
        //Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14));
    }


    [Fact]
    public void SkipOnRegisterPayment_WhenPaymentNotRequired()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = 0;
        var requiredFee = new Money(requiredFeeAmount);
        aggregate.AcceptApplicationForm(requiredFee);

        //Act
        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        //Assert
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.RequiredFee?.IsZero().Should().BeTrue();
    }

    [Fact]
    public void BePaidAndEmmitEvents_WhenPaymentNotBalance()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        //Act

        aggregate.RegisterFeePayment(requiredFee / 2, DateTime.UtcNow, 14);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(5);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationFeeNotBalancedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(5);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationFeePaymentRegistered>();
        aggregate.Status.Should().Be(ApplicationStatus.AwaitDecision);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);
        aggregate.PaidFee?.Amount.Should().Be(requiredFeeAmount / 2);
        aggregate.IsPaid.Should().BeFalse();
        aggregate
            .DecisionExpectDate.Should().NotHaveValue();
    }

    [Fact]
    public void ThrowOnAccept_WhenNotPaid()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);
        aggregate.RegisterFeePayment(requiredFee / 2, DateTime.UtcNow, 14);

        //Act
        // Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.ApproveApplication());
    }

    [Fact]
    public void ThrowOnAccept_WhenNotReccomended()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        aggregate.RegisterFeePayment(requiredFee / 2, DateTime.UtcNow, 14);

        //Act
        // Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.ApproveApplication());
    }

    [Fact]
    public void ChangeStatusAndEmmitEventsOnAccept_WhenRecommendedAndApproved()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);
        //Act

        aggregate.ApproveApplication();

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(6);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationApprovedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(6);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationApproved>();
        aggregate.Status.Should().Be(ApplicationStatus.Accepted);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);
    }

    [Fact]
    public void ChangeStatusAndEmmitEvents_WhenRejected()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var rejectionDate = DateTime.UtcNow;
        var decision = this._fixture.Create<string>();
        //Act

        aggregate.RejectApplication(rejectionDate,decision, 14);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(6);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationRejectedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(6);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationRejected>();
        aggregate.Status.Should().Be(ApplicationStatus.Rejected);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);
        aggregate.RejectDate.Should()
            .HaveValue()
            .And
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.RejectionDetails.Should().Be(decision);
        aggregate.AppealDeadline.Should().HaveValue().And.Be(DateTime.UtcNow.AddDays(14).Date);
    }

    [Fact]
    public void ThrowsOnReject_WhenNotRecommended()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();

        //Act
        // Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.RejectApplication(DateTime.UtcNow, decision, 14));
    }

    [Fact]
    public void ChangeStatusAndEmmitEvents_WhenRejectionAppealed()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();
        aggregate.RejectApplication(DateTime.UtcNow, decision, 14);

        var justification = this._fixture.Create<string>();
        //Act
        aggregate.AppealRejection(DateTime.UtcNow, justification);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(7);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationRejectionAppealReceivedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(7);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationRejectionAppealReceived>();
        aggregate.Status.Should().Be(ApplicationStatus.RejectionAppealed);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);
        aggregate.RejectDate.Should()
            .HaveValue()
            .And
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.RejectionDetails.Should().Be(decision);
        aggregate.AppealDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.AppealJustification.Should().Be(justification);
    }
    
    [Fact]
    public void DismissAppeal_WhenAppealedAfterDeadline()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();
        var rejectionDate = DateTime.UtcNow.AddDays(-16);
        aggregate.RejectApplication(rejectionDate, decision, 14);

        var justification = this._fixture.Create<string>();
        //Act
        aggregate.AppealRejection(DateTime.UtcNow, justification);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(7);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationRejectionAppealDismissedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(8);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationRejectionAppealReceived>();
        aggregate.Status.Should().Be(ApplicationStatus.AppealRejected);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);
        aggregate.RejectDate.Should()
            .HaveValue()
            .And
            .Be(rejectionDate);
        aggregate.RejectionDetails.Should().Be(decision);
        aggregate.AppealDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.AppealJustification.Should().Be(justification);
    }

    [Fact]
    public void ThrowOnAppeal_WhenNotRejected()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var justification = this._fixture.Create<string>();

        //Act
        // Assert
        Assert.Throws<AggregateInvalidStateException>(() => aggregate.AppealRejection(DateTime.UtcNow, justification));
    }

    [Fact]
    public void ChangeStatusAndEmmitEvents_WhenRejectionAppealApproved()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();
        aggregate.RejectApplication(DateTime.UtcNow,decision, 14);

        var justification = this._fixture.Create<string>();

        aggregate.AppealRejection(DateTime.UtcNow, justification);

        //Act

        aggregate.ApproveAppeal(DateTime.UtcNow);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(8);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationRejectionAppealApprovedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(8);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationRejectionAppealApproved>();
        aggregate.Status.Should().Be(ApplicationStatus.AppealSuccessful);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);
        aggregate.RejectDate.Should()
            .HaveValue()
            .And
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.RejectionDetails.Should().Be(decision);
        aggregate.AppealDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.AppealJustification.Should().Be(justification);
        aggregate.AppealApproveDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Fact]
    public void ThrowOnAppealAccept_WhenNotAppealed()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();
        aggregate.RejectApplication(DateTime.UtcNow,decision, 14);

        //Act
        //Assert
        Assert.Throws<AggregateInvalidStateException>(()=>aggregate.ApproveAppeal(DateTime.UtcNow));
    }
    
    [Fact]
    public void ChangeStatusAndEmmitEvents_WhenRejectionAppealDismissed()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();
        aggregate.RejectApplication(DateTime.UtcNow,decision, 14);

        var justification = this._fixture.Create<string>();

        aggregate.AppealRejection(DateTime.UtcNow, justification);

        var dismiss = this._fixture.Create<string>();
        //Act

        aggregate.DismissAppeal(DateTime.UtcNow, dismiss);

        // Assert
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GetDomainEvents().Should().HaveCount(8);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<ApplicationRejectionAppealDismissedDomainEvent>();
        aggregate.GetStateEvents().Should().HaveCount(8);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ApplicationRejectionAppealDismissed>();
        aggregate.Status.Should().Be(ApplicationStatus.AppealRejected);
        aggregate.IsValid.Should().BeTrue();
        aggregate.RequiredFee.Should().NotBeNull();
        aggregate.Recommendations.Should().HaveCount(1);
        aggregate.RejectDate.Should()
            .HaveValue()
            .And
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.RejectionDetails.Should().Be(decision);
        aggregate.AppealDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.AppealJustification.Should().Be(justification);
        aggregate.AppealRejectDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.AppealRejectionDetails.Should().Be(dismiss);
    }

    [Fact]
    public void ThrowsOnAppealDismiss_WhenNotAppealed()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();
        aggregate.RejectApplication(DateTime.UtcNow,decision, 14);
        
        var dismiss = this._fixture.Create<string>();
        //Act
        // Assert
        Assert.Throws<AggregateInvalidStateException>(()=>aggregate.DismissAppeal(DateTime.UtcNow, dismiss));
    }

    [Fact]
    public void RestoreState_WhenStateChangesProvided()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();
        aggregate.RejectApplication(DateTime.UtcNow,decision, 14);

        var justification = this._fixture.Create<string>();

        aggregate.AppealRejection(DateTime.UtcNow, justification);

        var dismiss = this._fixture.Create<string>();

        var events = new List<IStateEvent>();
        events.AddRange(aggregate.GetStateEvents());
        aggregate.ClearChanges();
        aggregate.ClearDomainEvents();

        //Act

        var newAggregate = new ApplicationForm(aggregate.AggregateId, this.tenantId, events);

        // Assert
        newAggregate.Should().BeEquivalentTo(aggregate);
    }

    [Fact]
    public void RestoreApprovedApplication()
    {
       
            // Arrange
            var firstName = this._fixture.Create<string>();
            var lastName = this._fixture.Create<string>();
            var email = this._fixture.Create<string>();
            var phone = this._fixture.Create<string>();
            var address = this._fixture.Create<string>();
            var recommendations = this._fixture.CreateMany<string>(1).ToList();

            var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

            var requiredFeeAmount = this._fixture.Create<decimal>();
            var requiredFee = new Money(requiredFeeAmount);

            aggregate.AcceptApplicationForm(requiredFee);

            var memberId = this._fixture.Create<Guid>();
            var memberNumber = this._fixture.Create<string>();
            aggregate.RequestRecommendation(memberId, memberNumber);

            var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
            aggregate.EndorseRecommendation(recommendation.Id, memberId);

            aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);
            aggregate.ApproveApplication();
            var events = new List<IStateEvent>();
            events.AddRange(aggregate.GetStateEvents());
            aggregate.ClearChanges();
            aggregate.ClearDomainEvents();

            //Act

            var newAggregate = new ApplicationForm(aggregate.AggregateId, this.tenantId, events);

            // Assert
            newAggregate.Should().BeEquivalentTo(aggregate);
    }
    
    [Fact]
    public void RestoreNotRecommendedApplication()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.RefuseRecommendation(recommendation.Id, memberId);

        var events = new List<IStateEvent>();
        events.AddRange(aggregate.GetStateEvents());
        aggregate.ClearChanges();
        aggregate.ClearDomainEvents();

        //Act

        var newAggregate = new ApplicationForm(aggregate.AggregateId, this.tenantId, events);

        // Assert
        newAggregate.Should().BeEquivalentTo(aggregate);
    }
    
    [Fact]
    public void RestoreCancelledApplication()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var problem = this._fixture.Create<string>();
        aggregate.CancelApplicationForm(problem);

        var events = new List<IStateEvent>();
        events.AddRange(aggregate.GetStateEvents());
        aggregate.ClearChanges();
        aggregate.ClearDomainEvents();

        //Act

        var newAggregate = new ApplicationForm(aggregate.AggregateId, this.tenantId, events);

        // Assert
        newAggregate.Should().BeEquivalentTo(aggregate);
    }
    
    [Fact]
    public void RestoreFailedAppeal()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();
        aggregate.RejectApplication(DateTime.UtcNow,decision, 14);

        var justification = this._fixture.Create<string>();

        aggregate.AppealRejection(DateTime.UtcNow, justification);

        var dismiss = this._fixture.Create<string>();

        aggregate.DismissAppeal(DateTime.UtcNow, dismiss);
        var events = new List<IStateEvent>();
        
        events.AddRange(aggregate.GetStateEvents());
        aggregate.ClearChanges();
        aggregate.ClearDomainEvents();

        //Act

        var newAggregate = new ApplicationForm(aggregate.AggregateId, this.tenantId, events);

        // Assert
        newAggregate.Should().BeEquivalentTo(aggregate);
    }
    
    [Fact]
    public void RestoreSuccessfulAppeal()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(1).ToList();

        var aggregate = new ApplicationForm(this.tenantId,firstName, lastName, email, phone, recommendations, address);

        var requiredFeeAmount = this._fixture.Create<decimal>();
        var requiredFee = new Money(requiredFeeAmount);

        aggregate.AcceptApplicationForm(requiredFee);

        var memberId = this._fixture.Create<Guid>();
        var memberNumber = this._fixture.Create<string>();
        aggregate.RequestRecommendation(memberId, memberNumber);

        var recommendation = aggregate.Recommendations.First(x => x.MemberId == memberId);
        aggregate.EndorseRecommendation(recommendation.Id, memberId);

        aggregate.RegisterFeePayment(requiredFee, DateTime.UtcNow, 14);

        var decision = this._fixture.Create<string>();
        aggregate.RejectApplication(DateTime.UtcNow,decision, 14);

        var justification = this._fixture.Create<string>();

        aggregate.AppealRejection(DateTime.UtcNow, justification);

        var dismiss = this._fixture.Create<string>();

        aggregate.ApproveAppeal(DateTime.UtcNow);
        var events = new List<IStateEvent>();
        
        events.AddRange(aggregate.GetStateEvents());
        aggregate.ClearChanges();
        aggregate.ClearDomainEvents();

        //Act

        var newAggregate = new ApplicationForm(aggregate.AggregateId, this.tenantId, events);

        // Assert
        newAggregate.Should().BeEquivalentTo(aggregate);
    }
}