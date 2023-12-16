using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ONPA.Common.Application;
using ONPA.Common.Queries;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Api.Controllers;
using ONPA.Membership.Api.Maps;
using ONPA.Membership.Contract.Requests;
using ONPA.Membership.Contract.Responses;

namespace PLUG.System.Apply.UnitTests.Application;

public class MembersControllerShould
{
    private readonly MembersController _sut;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IFixture _fixture;
    
    public MembersControllerShould()
    {
        this._fixture = new Fixture();
        this._mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MembershipMaps>();
            cfg.ShouldUseConstructor = ci => ci.IsPublic;
        }).CreateMapper();
        this._mediator = Substitute.For<IMediator>();
        this._sut = new MembersController(this._mediator, this._mapper);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberIsCreated()
    {
        // Arrange
        var request = this._fixture.Create<CreateMemberRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<CreateMemberCommand>()).Returns(response);
        // Act
        var result = await this._sut.CreateMember(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberIsUpdated()
    {
        // Arrange
        var request = this._fixture.Create<UpdateMemberDataRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<ModifyMemberContactDataCommand>()).Returns(response);
        // Act
        var result = await this._sut.UpdateMember(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberIsSuspended()
    {
        // Arrange
        var request = this._fixture.Create<MemberSuspensionRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<SuspendMemberCommand>()).Returns(response);
        // Act
        var result = await this._sut.SuspendMember(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberTypeIsChanged()
    {
        // Arrange
        var request = this._fixture.Create<ChangeMemberTypeRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<MakeMemberRegularCommand>()).Returns(response);
        // Act
        var result = await this._sut.ChangeMemberType(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberFeeIsRegistered()
    {
        // Arrange
        var request = this._fixture.Create<RegisterMembershipFeePaymentRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<RegisterMemberFeePaymentCommand>()).Returns(response);
        // Act
        var result = await this._sut.RegisterMembershipFee(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberIsRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetMemberRequest>();
        var response = this._fixture.Create<MemberResult>();
        
        this._mediator.Send(Arg.Any<GetMemberByIdQuery>()).Returns(response);
        // Act
        var result = await this._sut.GetMember(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<MemberResult>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMembersAreRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetMembersRequest>();
        var memberResults = this._fixture.CreateMany<MemberResult>();
        var response = CollectionResult<MemberResult>.FromValue(memberResults,memberResults.Count());
        
        this._mediator.Send(Arg.Any<GetMembersByStatusQuery>()).Returns(response);
        // Act
        var result = await this._sut.GetMembers(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<MemberResult>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberFeesAreRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetMemberFeesRequest>();
        var memberFeeResults = this._fixture.CreateMany<MemberFee>();
        var response = CollectionResult<MemberFee>.FromValue(memberFeeResults,memberFeeResults.Count());
        
        this._mediator.Send(Arg.Any<GetMemberFeesQuery>()).Returns(response);
        // Act
        var result = await this._sut.GetMemberFees(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<MemberFee>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberSuspensionAppealIsRegistered()
    {
        // Arrange
        var request = this._fixture.Create<MemberSuspensionAppealRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<AppealMemberSuspensionCommand>()).Returns(response);
        // Act
        var result = await this._sut.AppealSuspension(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberSuspensionAppealIsAccepted()
    {
        // Arrange
        var request = this._fixture.Create<AcceptMemberSuspensionAppealRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<AcceptSuspensionAppealCommand>()).Returns(response);
        // Act
        var result = await this._sut.AcceptAppealSuspension(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberSuspensionAppealIsDismissed()
    {
        // Arrange
        var request = this._fixture.Create<RejectMemberSuspensionAppealRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<DismissSuspensionAppealCommand>()).Returns(response);
        // Act
        var result = await this._sut.DismissSuspension(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
}