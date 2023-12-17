using AutoFixture;
using MediatR;
using NSubstitute;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Api.Application.IntegrationEvents.EventHandlers;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Api.Application.Services;
using ONPA.Membership.Contract.Responses;

namespace ONPA.UnitTests.Application.IntegrationEventHandlers;

public class MemberIntegrationEventHandlersTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IMediator _mediator;
    
    public MemberIntegrationEventHandlersTests()
    {
        this._mediator = Substitute.For<IMediator>();
    }
    
    [Fact]
    public async Task AllMembershipFeeRequestedIntegrationEventHandler_Handle_ShouldSendRequestMemberFeePaymentCommand()
    {
        // Arrange
        var @event = this._fixture.Create<AllMembershipFeeRequestedIntegrationEvent>();
        var sut = new AllMembershipFeeRequestedIntegrationEventHandler(this._mediator);
        var memberIdResult = this._fixture.CreateMany<MemberIdResult>(10);
        this._mediator.Send(Arg.Any<GetAllActiveRegularMembersQuery>()).Returns(
             CollectionResult<MemberIdResult>.FromValue(memberIdResult,10));
        
        // Act
        await sut.Handle(@event);
        
        // Assert
        await this._mediator.Received(10).Send(Arg.Any<RequestMemberFeePaymentCommand>());
    }
    
   
    [Fact]
    public async Task MemberJoinedIntegrationEventHandler_Handle_ShouldCreateMember()
    {
        // Arrange
        var @event = this._fixture.Create<MemberJoinedIntegrationEvent>();
        var sut = new MemberJoinedIntegrationEventHandler(this._mediator);
        
        // Act
        await sut.Handle(@event);
        
        // Assert
        await this._mediator.Received(1).Send(Arg.Any<CreateMemberCommand>());
    }
    
    [Fact]
    public async Task ApplicationReceivedIntegrationEventHandler_Handle_ShouldValidateRecommendingMembers()
    {
        // Arrange
        var @event = this._fixture.Create<ApplicationReceivedIntegrationEvent>();
        IMemberRecommendationValidationService memberRecommendationValidationService = Substitute.For<IMemberRecommendationValidationService>();
        var sut = new ApplicationReceivedIntegrationEventHandler(memberRecommendationValidationService);
        
        // Act
        await sut.Handle(@event);
        
        // Assert
        await memberRecommendationValidationService.ReceivedWithAnyArgs(1)
            .ValidateRecommendingMembers(Arg.Any<Guid>(),
                Arg.Any<Guid>(),
                default!);   
    }
    
}