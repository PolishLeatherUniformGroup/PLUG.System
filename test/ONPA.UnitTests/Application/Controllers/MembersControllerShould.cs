using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ONPA.Common.Application;
using ONPA.Common.Queries;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Api.Controllers;
using ONPA.Membership.Api.Maps;
using ONPA.Membership.Api.Services;
using ONPA.Membership.Contract.Requests;
using ONPA.Membership.Contract.Requests.Dtos;
using ONPA.Membership.Contract.Responses;

namespace ONPA.UnitTests.Application.Controllers;

public class MembersControllerShould
{
    private readonly MembersController _sut;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IIdentityService _identityService;
    private readonly IFixture _fixture;
    private readonly Guid _tenantId = Guid.NewGuid();
    
    public MembersControllerShould()
    {
        this._fixture = new Fixture();
        this._mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MembershipMaps>();
            cfg.ShouldUseConstructor = ci => ci.IsPublic;
        }).CreateMapper();
        this._mediator = Substitute.For<IMediator>();
        this._identityService = Substitute.For<IIdentityService>();
        this._identityService.GetUserOrganization().Returns(this._tenantId);
        this._sut = new MembersController(this._mediator, this._mapper, this._identityService);
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
    
    [Theory()]
    [InlineData(1)]
    [InlineData(2)]
    public async Task ReturnOkWhenMemberTypeIsChanged(int type)
    {
        // Arrange
        var request = this._fixture.Build<ChangeMemberTypeRequest>()
            .With(x => x.MemberType,
                this._fixture.Build<MemberType>().With(c=>c.Type,type).Create())
            .Create();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<MakeMemberRegularCommand>()).Returns(response);
        this._mediator.Send(Arg.Any<MakeMemberHonoraryCommand>()).Returns(response);
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
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberFeesAreRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetMemberFeesRequest>();
        var memberFeeResults = this._fixture.CreateMany<MemberFeeResult>();
        var response = CollectionResult<MemberFeeResult>.FromValue(memberFeeResults,memberFeeResults.Count());
        
        this._mediator.Send(Arg.Any<GetMemberFeesQuery>()).Returns(response);
        // Act
        var result = await this._sut.GetMemberFees(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<MemberFeeResult>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberExpelAppealIsRegistered()
    {
        // Arrange
        var request = this._fixture.Create<MemberExpelAppealRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<AppealMemberExpelCommand>()).Returns(response);
        // Act
        var result = await this._sut.AppealExpel(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberExpelAppealIsAccepted()
    {
        // Arrange
        var request = this._fixture.Create<AcceptMemberExpelAppealRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<AcceptExpelAppealCommand>()).Returns(response);
        // Act
        var result = await this._sut.AcceptAppealExpel(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }
    
    [Fact]
    public async Task ReturnOkWhenMemberExpelAppealIsDismissed()
    {
        // Arrange
        var request = this._fixture.Create<RejectMemberExpelAppealRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<DismissExpelAppealCommand>()).Returns(response);
        // Act
        var result = await this._sut.DismissAppealExpel(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
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
    [Fact]
    public async Task ReturnOkWhenMemberIsExpelled()
    {
        // Arrange
        var request = this._fixture.Create<MemberExpelRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<ExpelMemberCommand>()).Returns(response);
        // Act
        var result = await this._sut.ExpelsMember(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }

    [Fact]
    public async Task ReturnOkWhenMemberIsExpired()
    {
        // Arrange
        var request = this._fixture.Create<MemberExpirationRequest>();
        var response = this._fixture.Create<Guid>();
        
        this._mediator.Send(Arg.Any<ExpireMembershipCommand>()).Returns(response);
        // Act
        var result = await this._sut.ExpireMembership(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().Be(response);
    }

    [Fact]
    public async Task ReturnsSuspensionHistory()
    {
        // Arrange
        var request = this._fixture.Create<GetMemberSuspensionsRequest>();
        var memberResults = this._fixture.CreateMany<MemberSuspensionResult>();
        var response = CollectionResult<MemberSuspensionResult>.FromValue(memberResults,memberResults.Count());
        
        this._mediator.Send(Arg.Any<GetMemberSuspensionsQuery>()).Returns(response);
        // Act
        var result = await this._sut.SuspensionHistory(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<MemberSuspensionResult>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnsExpelHistory()
    {
        // Arrange
        var request = this._fixture.Create<GetMemberExpelsRequest>();
        var memberResults = this._fixture.CreateMany<MemberExpelResult>();
        var response = CollectionResult<MemberExpelResult>.FromValue(memberResults,memberResults.Count());
        
        this._mediator.Send(Arg.Any<GetMemberExpelsQuery>()).Returns(response);
        // Act
        var result = await this._sut.ExpelHistory(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<MemberExpelResult>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
}