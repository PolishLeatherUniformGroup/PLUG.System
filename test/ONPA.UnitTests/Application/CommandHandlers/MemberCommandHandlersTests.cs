using AutoFixture;
using FluentAssertions;
using NSubstitute;
using ONPA.Common.Domain;
using ONPA.Membership.Api.Application.CommandHandlers;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Contract.Requests;
using ONPA.Membership.Domain;
using PLUG.System.SharedDomain;
using PLUG.System.SharedDomain.Helpers;

namespace ONPA.UnitTests.Application.CommandHandlers;

public class MemberCommandHandlersTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IAggregateRepository<Member> _organizationRepository;
    private readonly Member aggregate;
    private readonly Guid tenantId = Guid.NewGuid();

    public MemberCommandHandlersTests()
    {
        this._organizationRepository = Substitute.For<IAggregateRepository<Member>>();

        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var number = new CardNumber(this._fixture.Create<int>());
        var join = this._fixture.Create<DateTime>();
        var paidFee = new Money(this._fixture.Create<decimal>());
        // act 
        this.aggregate = new Member(this.tenantId, number, firstName, lastName, email, phone, address, join, paidFee);
        this._organizationRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(this.aggregate);
    }

    [Fact]
    public async Task CreateMemberCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<CreateMemberCommand>();
        var sut = new CreateMemberCommandHandler(this._organizationRepository);

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
    public async Task UpdateMemberCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ModifyMemberContactDataCommand>();
        var sut = new ModifyMemberContactDataCommandHandler(this._organizationRepository);

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
    public async Task MakeMemberHonoraryCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<MakeMemberHonoraryCommand>();
        var sut = new MakeMemberHonoraryCommandHandler(this._organizationRepository);

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
    public async Task MakeMemberRegularCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<MakeMemberRegularCommand>();
        var sut = new MakeMemberRegularCommandHandler(this._organizationRepository);
        this.aggregate.MakeHonoraryMember();
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
    public async Task RequestMemberFeePaymentCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<RequestMemberFeePaymentCommand>();
        var sut = new RequestMemberFeePaymentCommandHandler(this._organizationRepository);

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
    public async Task RegisterMemberFeePaymentCommandShouldBeHandled()
    {
        // Arrange

        var sut = new RegisterMemberFeePaymentCommandHandler(this._organizationRepository);
        this.aggregate.RequestFeePayment(new Money(100), DateTime.Now, DateTime.Now.AddYears(1).ToYearEnd());
        var command = this._fixture.Build<RegisterMemberFeePaymentCommand>()
            .With(x => x.FeeAmount, new Money(100))
            .With(x => x.FeeId, this.aggregate.CurrentFee?.Id)
            .Create();
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
    public async Task RegisterMemberFeePaymentCommandShouldFail()
    {
        // Arrange

        var sut = new RegisterMemberFeePaymentCommandHandler(this._organizationRepository);
        var command = this._fixture.Build<RegisterMemberFeePaymentCommand>()
            .With(x => x.FeeAmount, new Money(100))
            .With(x => x.FeeId, Guid.NewGuid)
            .Create();
        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
        result.AggregateId.Should().Be(Guid.Empty);
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task SuspendMemberCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<SuspendMemberCommand>();
        var sut = new SuspendMemberCommandHandler(this._organizationRepository);

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
    public async Task AppealMemberSuspensionCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<AppealMemberSuspensionCommand>();
        var sut = new AppealMemberSuspensionCommandHandler(this._organizationRepository);
        this.aggregate.SuspendMember(this._fixture.Create<string>(),DateTime.UtcNow, this._fixture.Create<DateTime>(), 14);
        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().Be(this.aggregate.AggregateId);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task AcceptAppealMemberSuspensionCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<AcceptSuspensionAppealCommand>();
        var sut = new AcceptSuspensionAppealCommandHandler(this._organizationRepository);
        this.aggregate.SuspendMember(this._fixture.Create<string>(),DateTime.UtcNow, this._fixture.Create<DateTime>(), 14);
        this.aggregate.AppealSuspension(this._fixture.Create<string>(), DateTime.UtcNow.AddDays(1));
        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().Be(this.aggregate.AggregateId);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task DismissAppealMemberSuspensionCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<DismissSuspensionAppealCommand>();
        var sut = new DismissSuspensionAppealCommandHandler(this._organizationRepository);
        this.aggregate.SuspendMember(this._fixture.Create<string>(),DateTime.UtcNow, this._fixture.Create<DateTime>(), 14);
        this.aggregate.AppealSuspension(this._fixture.Create<string>(), DateTime.UtcNow.AddDays(1));
        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().Be(this.aggregate.AggregateId);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task ExpelMemberCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ExpelMemberCommand>();
        var sut = new ExpelMemberCommandHandler(this._organizationRepository);

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
    public async Task AppealMemberExpelCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<AppealMemberExpelCommand>();
        var sut = new AppealMemberExpelCommandHandler(this._organizationRepository);
        this.aggregate.ExpelMember(this._fixture.Create<string>(),DateTime.UtcNow,  14);
        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().Be(this.aggregate.AggregateId);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task AcceptAppealMemberExpelCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<AcceptExpelAppealCommand>();
        var sut = new AcceptExpelAppealCommandHandler(this._organizationRepository);
        this.aggregate.ExpelMember(this._fixture.Create<string>(),DateTime.UtcNow,  14);
        this.aggregate.AppealExpel(this._fixture.Create<string>(), DateTime.UtcNow.AddDays(1));
        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().Be(this.aggregate.AggregateId);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task DismissAppealMemberExpelCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<DismissExpelAppealCommand>();
        var sut = new DismissExpelAppealCommandHandler(this._organizationRepository);
        this.aggregate.ExpelMember(this._fixture.Create<string>(),DateTime.UtcNow,  14);
        this.aggregate.AppealExpel(this._fixture.Create<string>(), DateTime.UtcNow.AddDays(1));
        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().Be(this.aggregate.AggregateId);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    
    [Fact]
    public async Task ExpireMemberCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ExpireMembershipCommand>();
        var sut = new ExpireMembershipCommandHandler(this._organizationRepository);

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
    public async Task ReactivateMemberCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ReactivateMemberCommand>();
        var sut = new ReactivateMemberCommandHandler(this._organizationRepository);
        this.aggregate.MembershipExpired(this._fixture.Create<DateTime>(), this._fixture.Create<string>());
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
    public async Task MemberLeavingCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<MemberLeavingCommand>();
        var sut = new MemberLeavingCommandHandler(this._organizationRepository);
        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().BeEmpty();
        result.AggregateId.Should().Be(this.aggregate.AggregateId);
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
}