using AutoFixture;
using NSubstitute;
using ONPA.Common.Domain;
using ONPA.IntegrationEvents;
using ONPA.Membership.Api.Application.DomainEventHandlers;
using ONPA.Membership.Api.Application.IntegrationEvents;
using ONPA.Membership.Domain;
using ONPA.Membership.DomainEvents;
using PLUG.System.SharedDomain;

namespace ONPA.UnitTests.Application.DomainEventHandlers;

public class MemberDomainEventHandlersTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IAggregateRepository<MembersGroup> _groupAggregateRepository;
    private readonly IAggregateRepository<Member> _memberAggregateRepository;
    private readonly IIntegrationEventService _integrationEventService;
    private Guid _tenantId = Guid.NewGuid();
    private MembersGroup _groupAggregate;
    private Member _memberAggregate;
    private Guid tenantId=Guid.NewGuid();

    public MemberDomainEventHandlersTests()
    {
        _groupAggregateRepository = Substitute.For<IAggregateRepository<MembersGroup>>();
        _memberAggregateRepository = Substitute.For<IAggregateRepository<Member>>();
        _integrationEventService = Substitute.For<IIntegrationEventService>();
        _groupAggregate = new MembersGroup(this._tenantId, this._fixture.Create<string>(), MembersGroupType.CustomGroup);
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var number = new CardNumber(this._fixture.Create<int>());
        var join = this._fixture.Create<DateTime>();
        var paidFee = new Money(this._fixture.Create<decimal>());
         _memberAggregate = new Member(this.tenantId,number, firstName, lastName, email, phone, address, join, paidFee);

        _groupAggregateRepository.GetByIdAsync(Arg.Is<Guid>(x => x == _groupAggregate.AggregateId), Arg.Any<CancellationToken>()).Returns(_groupAggregate);
        _memberAggregateRepository.GetByIdAsync(Arg.Is<Guid>(x => x == _memberAggregate.AggregateId), Arg.Any<CancellationToken>()).Returns(_memberAggregate);
    }
    
    [Fact]
    public async Task MemberExpelAppealDismissedDomainEventHandler_Handle()
    {
        // Arrange
        var groupMemberships =new List<Guid> {_groupAggregate.AggregateId};
        var domainEvent = new MemberExpelAppealDismissedDomainEvent(
            new CardNumber(this._fixture.Create<int>()),
            this._fixture.Create<string>(),
            this._fixture.Create<string>(),
            this._fixture.Create<DateTime>(),
            this._fixture.Create<string>(),
            groupMemberships);
        domainEvent.WithAggregate(this._fixture.Create<Guid>(), this._fixture.Create<Guid>());
        _groupAggregate.JoinGroup(domainEvent.MemberNumber,Guid.NewGuid(),domainEvent.FirstName,this._fixture.Create<string>(),this._fixture.Create<DateTime>());
        var handler = new MemberExpelAppealDismissedDomainEventHandler(_groupAggregateRepository, _integrationEventService);
        
        // Act
        await handler.Handle(domainEvent, CancellationToken.None);
        
        // Assert
        await _groupAggregateRepository.Received(1).GetByIdAsync(_groupAggregate.AggregateId, Arg.Any<CancellationToken>());
        await _groupAggregateRepository.Received(1).UpdateAsync(_groupAggregate, Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task MemberFeePaymentRegisteredDomainEventHandler_Handle()
    {
        // Arrange
        var domainEvent = new MemberFeePaymentRegisteredDomainEvent(
            this._fixture.Create<string>(),
            this._fixture.Create<string>(),
            new Money(this._fixture.Create<decimal>()),
            new Money(this._fixture.Create<decimal>()),
            this._fixture.Create<DateTime>(),
            this._fixture.Create<DateTime>());
        
        domainEvent.WithAggregate(this._fixture.Create<Guid>(), this._fixture.Create<Guid>());
        var handler = new MemberFeePaymentRegisteredDomainEventHandler(_integrationEventService);
        
        // Act
        await handler.Handle(domainEvent, CancellationToken.None);
        
        // Assert
        await _integrationEventService.Received(1).AddAndSaveEventAsync(Arg.Any<MemberFeePaymentIncompleteIntegrationEvent>());
    }
    
    [Fact]
    public async Task MemberFeePaymentRequiredDomainEventHandler_Handle()
    {
        // Arrange
        var domainEvent = new MemberFeePaymentRequestedDomainEvent(
            this._fixture.Create<string>(),
            this._fixture.Create<string>(),
            new Money(this._fixture.Create<decimal>()),
            this._fixture.Create<DateTime>(),
            this._fixture.Create<DateTime>());
        
        domainEvent.WithAggregate(this._fixture.Create<Guid>(), this._fixture.Create<Guid>());
        var handler = new MemberFeePaymentRequestedDomainEventHandler(_integrationEventService);
        
        // Act
        await handler.Handle(domainEvent, CancellationToken.None);
        
        // Assert
        await _integrationEventService.Received(1).AddAndSaveEventAsync(Arg.Any<MemberRequestedFeePaymentIntegrationEvent>());
    }

    [Fact]
    public async Task MemberJoinedGroupDomainEventHandler_Handle()
    {
        // Arrange
        var domainEvent = new MemberJoinedGroupDomainEvent(
            _memberAggregate.AggregateId);
        
        domainEvent.WithAggregate(this._fixture.Create<Guid>(), this._fixture.Create<Guid>());
        var handler = new MemberJoinedGroupDomainEventHandler(_memberAggregateRepository);
        
        // Act
        await handler.Handle(domainEvent, CancellationToken.None);
        
        // Assert
        await _memberAggregateRepository.Received(1).GetByIdAsync(_memberAggregate.AggregateId, Arg.Any<CancellationToken>());
        await _memberAggregateRepository.Received(1).UpdateAsync(_memberAggregate, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task MemberJoinedDomainEventHandler_Handle()
    {
        // Arrange
        var domainEvent = new MemberJoinedDomainEvent(
            _memberAggregate.MemberNumber,
            _memberAggregate.FirstName,
            _memberAggregate.LastName,
            _memberAggregate.Email,
            _memberAggregate.Phone);

        domainEvent.WithAggregate(this._fixture.Create<Guid>(), this._fixture.Create<Guid>());
        var handler = new MemberJoinedDomainEventHandler(_integrationEventService);

        // Act
        await handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        await _integrationEventService.Received(1).AddAndSaveEventAsync(Arg.Any<MemberCardNumberAssignedIntegrationEvent>());
    }

    [Fact]
    public async Task MemberExpelledDomainEventHandler_Handle()
    {
        // Arrange
        var domainEvent = new MemberLeftDomainEvent(
            _memberAggregate.MemberNumber,
            _memberAggregate.FirstName,
            _memberAggregate.Email,
            this._fixture.Create<DateTime>(),
            new List<Guid>(){_groupAggregate.AggregateId});
        _groupAggregate.JoinGroup(_memberAggregate.MemberNumber,Guid.NewGuid(),domainEvent.FirstName,this._fixture.Create<string>(),this._fixture.Create<DateTime>());

        domainEvent.WithAggregate(this._fixture.Create<Guid>(), this._fixture.Create<Guid>());
        var handler = new MemberLeftDomainEventHandler(_groupAggregateRepository, _integrationEventService);

        // Act
        await handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        await _groupAggregateRepository.Received(1).GetByIdAsync(_groupAggregate.AggregateId, Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task MemberLeftGroupDomainEventHandler_Handle()
    {
        // Arrange
        var domainEvent = new MemberLeftGroupDomainEvent(
            _memberAggregate.AggregateId);
        
        domainEvent.WithAggregate(this._fixture.Create<Guid>(), this._fixture.Create<Guid>());
        var handler = new MemberLeftGroupDomainEventHandler(_memberAggregateRepository);
        
        // Act
        await handler.Handle(domainEvent, CancellationToken.None);
        
        // Assert
        await _memberAggregateRepository.Received(1).GetByIdAsync(_memberAggregate.AggregateId, Arg.Any<CancellationToken>());
        await _memberAggregateRepository.Received(1).UpdateAsync(_memberAggregate, Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task MembershipExtendedDomainEventHandler_Handle()
    {
        // Arrange
        var domainEvent = new MembershipExtendedDomainEvent(
            _memberAggregate.FirstName,
            _memberAggregate.Email,
            this._fixture.Create<DateTime>());
        
        domainEvent.WithAggregate(this._fixture.Create<Guid>(), this._fixture.Create<Guid>());
        var handler = new MembershipExtendedDomainEventHandler(_integrationEventService);
        
        // Act
        await handler.Handle(domainEvent, CancellationToken.None);
        
        // Assert
        await _integrationEventService.Received(1).AddAndSaveEventAsync(Arg.Any<MembershipExtendedIntegrationEvent>());
    }
    
}