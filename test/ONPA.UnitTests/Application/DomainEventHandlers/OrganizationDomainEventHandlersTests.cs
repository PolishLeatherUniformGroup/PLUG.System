using AutoFixture;
using NSubstitute;
using ONPA.IntegrationEvents;
using ONPA.Organizations.Api.Application.DomainEventHandlers;
using ONPA.Organizations.Api.Application.IntegrationEvents;
using ONPA.Organizations.Domain;
using ONPA.Organizations.DomainEvents;
using PLUG.System.SharedDomain;

namespace ONPA.UnitTests.Application.DomainEventHandlers;

public class OrganizationDomainEventHandlersTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IIntegrationEventService _integrationEventService;
    
    public OrganizationDomainEventHandlersTests()
    {
        _integrationEventService = Substitute.For<IIntegrationEventService>();
    }
    
    [Fact]
    public async Task MembershipFeeRequestedDomainEventHandler_Handle()
    {
        // Arrange
        var domainEvent = new MembershipFeeRequestedDomainEvent(
            new MembershipFee(this._fixture.Create<int>(),
            new Money(this._fixture.Create<decimal>())),
            this._fixture.Create<DateTime>());
        var handler = new MembershipFeeRequestedDomainEventHandler(_integrationEventService);
        
        // Act
        await handler.Handle(domainEvent, CancellationToken.None);
        
        // Assert
        await _integrationEventService.Received(1).AddAndSaveEventAsync(Arg.Any<AllMembershipFeeRequestedIntegrationEvent>());
    }
}