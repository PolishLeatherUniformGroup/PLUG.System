using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ONPA.Apply.Api.Application.Commands;
using ONPA.Apply.Api.Application.Queries;
using ONPA.Apply.Api.Controllers;
using ONPA.Apply.Api.Maps;
using ONPA.Apply.Api.Services;
using ONPA.Apply.Contract.Requests;
using ONPA.Apply.Contract.Responses;
using ONPA.Common.Application;
using ONPA.Common.Queries;

namespace ONPA.UnitTests.Application.Controllers;

public class ApplicationsControllerShould
{
    private readonly ApplicationsController _sut;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly IFixture _fixture;
    private readonly Guid _tenantId = Guid.NewGuid();
    
    public ApplicationsControllerShould()
    {
        this._fixture = new Fixture();
        this._mediator = Substitute.For<IMediator>();
        this._mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ApplyMaps>();
            cfg.ShouldUseConstructor = ci => ci.IsPublic;
        }).CreateMapper();
        this._identityService = Substitute.For<IIdentityService>();
        this._sut = new ApplicationsController(this._mediator, this._mapper, this._identityService);
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationIsCreated()
    {
        // Arrange
        var request = this._fixture.Create<CreateApplicationRequest>();
        var respponse = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<CreateApplicationFormCommand>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.Create(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationIsRefused()
    {
        // Arrange
        var request = this._fixture.Create<RefuseRecommendationRequest>();
        var respponse = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<RefuseApplicationRecommendationCommand>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.RefuseRecommendation(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationIsEndorsed()
    {
        // Arrange
        var request = this._fixture.Create<EndorseRecommendationRequest>();
        var respponse = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<EndorseApplicationRecommendationCommand>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.EndorseRecommendation(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationIsApproved()
    {
        // Arrange
        var request = this._fixture.Create<ApproveApplicationRequest>();
        var respponse = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<ApproveApplicationCommand>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.ApproveApplication(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationIsRejected()
    {
        // Arrange
        var request = this._fixture.Create<RejectApplicationRequest>();
        var respponse = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<RejectApplicationCommand>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.RejectApplication(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationPaymentIsRegistered()
    {
        // Arrange
        var request = this._fixture.Create<RegisterApplicationPaymentRequest>();
        var respponse = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<RegisterApplicationFeePaymentCommand>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.RegisterPayment(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationRejectionIsAppealed()
    {
        // Arrange
        var request = this._fixture.Create<AppealApplicationRejectionRequest>();
        var respponse = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<AppealApplicationRejectionCommand>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.AppealRejection(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationRejectionAppealIsApproved()
    {
        // Arrange
        var request = this._fixture.Create<ApproveAppealApplicationRejectionRequest>();
        var respponse = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<ApproveApplicationRejectionAppealCommand>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.ApproveAppealRejection(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationRejectionAppealIsRejected()
    {
        // Arrange
        var request = this._fixture.Create<RejectAppealApplicationRejectionRequest>();
        var respponse = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<DismissApplicationRejectionAppealCommand>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.RejectAppealRejection(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationIsRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetApplicationRequest>();
        var respponse = this._fixture.Create<ApplicationDetails>();
        this._mediator.Send(Arg.Any<GetApplicationQuery>(), Arg.Any<CancellationToken>())
            .Returns(respponse);
        
        // Act
        var result = await this._sut.GetApplication(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<ApplicationDetails?>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkResultWhenApplicationsAreRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetApplicationsRequest>();
        var respponse = this._fixture.CreateMany<ApplicationResult>(5);
        this._mediator.Send(Arg.Any<GetApplicationsByStatusQuery>(), Arg.Any<CancellationToken>())
            .Returns(CollectionResult<ApplicationResult>.FromValue(respponse,5));
        
        // Act
        var result = await this._sut.GetApplications(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<ApplicationResult>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
}