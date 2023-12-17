using AutoFixture;
using FluentAssertions;
using NSubstitute;
using ONPA.Common.Domain;
using ONPA.Gatherings.Api.Application.CommandHandlers;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.UnitTests.Application.CommandHandlers;

public class EventCommandHandlersTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IAggregateRepository<Event> _aggregateRepository;
    private readonly Event _aggregate;
    
    private readonly Guid _tenantId = Guid.NewGuid();
    
    public EventCommandHandlersTests()
    {
        _aggregateRepository = Substitute.For<IAggregateRepository<Event>>();
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
       
        this._aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        
        this._aggregateRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(this._aggregate);
    }
    
    [Fact]
    public async Task CreateEventCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<CreateEventCommand>();
        var sut = new CreateEventCommandHandler(this._aggregateRepository);
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task ModifyEventDescriptionCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ModifyEventDescriptionCommand>();
        var sut = new ModifyEventDescriptionCommandHandler(this._aggregateRepository);
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task ModifyEventPriceCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ModifyEventPriceCommand>();
        var sut = new ModifyEventPriceCommandHandler(this._aggregateRepository);
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task ModifyEventCapacityCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ModifyEventCapacityCommand>();
        var sut = new ModifyEventCapacityCommandHandler(this._aggregateRepository);
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task ModifyEventScheduleCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ModifyEventScheduleCommand>();
        var sut = new ModifyEventScheduleCommandHandler(this._aggregateRepository);
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task AcceptEventCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<AcceptEventCommand>();
        var sut = new AcceptEventCommandHandler(this._aggregateRepository);
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task PublishEventCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<PublishEventCommand>();
        var sut = new PublishEventCommandHandler(this._aggregateRepository);
        this._aggregate.Accept();
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task CancelEventCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<CancelEventCommand>();
        var sut = new CancelEventCommandHandler(this._aggregateRepository);
        this._aggregate.Accept();
        this._aggregate.Publish();
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task ArchiveEventCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<ArchiveEventCommand>();
        var sut = new ArchiveEventCommandHandler(this._aggregateRepository);
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.AggregateId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task EnrollEventCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<EnrollToEventCommand>();
        var sut = new EnrollToEventCommandHandler(this._aggregateRepository);
        this._aggregate.Accept();
        this._aggregate.Publish();
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task CancelEnrollmentCommandShouldBeHandled()
    {
        // Arrange
        
        var sut = new CancelEnrollmentCommandHandler(this._aggregateRepository);
        this._aggregate.Accept();
        this._aggregate.Publish();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        this._aggregate.Enroll(DateTime.UtcNow, 1, firstName, lastName, email, new Participant[]
        {
            new(firstName, lastName, email)
        });
        var command = this._fixture.Build<CancelEnrollmentCommand>()
            .With(x => x.EnrollmentId, this._aggregate.Registrations.First().Id)
            .Create();
            
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task RegisterEnrollmentPaymentCommandShouldBeHandled()
    {
        // Arrange
        
        var sut = new RegisterEnrollmentPaymentCommandHandler(this._aggregateRepository);
        this._aggregate.Accept();
        this._aggregate.Publish();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        this._aggregate.Enroll(DateTime.UtcNow, 1, firstName, lastName, email, new Participant[]
        {
            new(firstName, lastName, email)
        });
        var command = this._fixture.Build<RegisterEnrollmentPaymentCommand>()
            .With(x => x.EnrollmentId, this._aggregate.Registrations.First().Id)
            .With(x=>x.PaidAmount, this._aggregate.PricePerPerson)
            .Create();
            
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
    
    [Fact]
    public async Task RefundEnrollmentPaymentCommandShouldBeHandled()
    {
        // Arrange
        
        var sut = new RefundCancelledEnrollmentCommandHandler(this._aggregateRepository);
        this._aggregate.Accept();
        this._aggregate.Publish();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        this._aggregate.Enroll(DateTime.UtcNow, 1, firstName, lastName, email, new Participant[]
        {
            new(firstName, lastName, email)
        });
        var command = this._fixture.Build<RefundCancelledEnrollmentCommand>()
            .With(x => x.EnrollmentId, this._aggregate.Registrations.First().Id)
            .With(x=>x.RefundedAmount, this._aggregate.PricePerPerson)
            .Create();
            
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }
}