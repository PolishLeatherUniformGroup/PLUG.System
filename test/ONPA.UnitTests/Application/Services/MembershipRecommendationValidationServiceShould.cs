using AutoFixture;
using MediatR;
using NSubstitute;
using ONPA.Membership.Api.Application.IntegrationEvents;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Api.Application.Services;
using ONPA.Membership.Contract.Responses;

namespace ONPA.UnitTests.Application.Services;

public class MembershipRecommendationValidationServiceShould
{
    private readonly IFixture _fixture = new Fixture();
    private readonly MemberRecommendationValidationService _service;
    private readonly IMediator _mediator;
    private readonly IIntegrationEventService _integrationEventService;
    
    public MembershipRecommendationValidationServiceShould()
    {
        _mediator = Substitute.For<IMediator>();
        _integrationEventService = Substitute.For<IIntegrationEventService>();
        _service = new MemberRecommendationValidationService(this._mediator, this._integrationEventService);
        
    }
    
    [Fact]
    public async Task ValidateRecommendingMembers()
    {
        // Arrange
        var tenantId = this._fixture.Create<Guid>();
        var applicationId = this._fixture.Create<Guid>();
        var memberNumbers = new List<string> {this._fixture.Create<string>()};
        var query = new ValidateMemberNumberQuery(tenantId, memberNumbers[0]);
        var result = new MemberValidationResult(this._fixture.Create<Guid>(), memberNumbers[0]);
        _mediator.Send(Arg.Is<ValidateMemberNumberQuery>(x => x.TenantId == tenantId && x.MemberNumber == memberNumbers[0]), Arg.Any<CancellationToken>()).Returns(result);
        
        // Act
        await _service.ValidateRecommendingMembers(tenantId, applicationId, memberNumbers.ToArray());
        
        // Assert
        await _mediator.Received(1).Send(Arg.Is<ValidateMemberNumberQuery>(x => x.TenantId == tenantId && x.MemberNumber == memberNumbers[0]), Arg.Any<CancellationToken>());
    }
}