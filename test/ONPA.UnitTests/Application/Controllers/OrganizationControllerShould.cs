using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ONPA.Common.Application;
using ONPA.Common.Queries;
using ONPA.Organizations.Api.Application.Commands;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Api.Controllers;
using ONPA.Organizations.Api.Maps;
using ONPA.Organizations.Contract.Requests;
using ONPA.Organizations.Contract.Responses;

namespace ONPA.UnitTests.Application.Controllers;

public class OrganizationControllerShould
{
    private readonly OrganizationsController _sut;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IFixture _fixture;

    public OrganizationControllerShould()
    {
        this._fixture = new Fixture();
        this._mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrganizationsMaps>();
            cfg.ShouldUseConstructor = ci => ci.IsPublic;
        }).CreateMapper();
        this._mediator = Substitute.For<IMediator>();
        this._sut = new OrganizationsController(this._mediator, this._mapper);
    }

    [Fact]
    public async Task Return_Organization_When_GetOrganizationById()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        var organization = this._fixture.Create<OrganizationResponse>();
        this._mediator.Send(Arg.Is<GetOrganizationQuery>(x => x.OrganizationId == organizationId))
            .Returns(organization);
        var request = this._fixture.Build<GetOrganizationRequest>()
            .With(x => x.OrganizationId, organizationId)
            .Create();

        // Act
        var result = await this._sut.GetOrganization(request);

        // Assert
        result.Should().BeOfType<ActionResult<OrganizationResponse?>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(organization);
    }

    [Fact]
    public async Task Return_NotFound_When_GetOrganizationByIdReturnsNull()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        var organization = this._fixture.Create<OrganizationResponse>();
        this._mediator.Send(Arg.Is<GetOrganizationQuery>(x => x.OrganizationId == organizationId))
            .Returns(null as OrganizationResponse);
        var request = this._fixture.Build<GetOrganizationRequest>()
            .With(x => x.OrganizationId, organizationId)
            .Create();

        // Act
        var result = await this._sut.GetOrganization(request);

        // Assert
        result.Should().BeOfType<ActionResult<OrganizationResponse?>>();
        result.Result.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async Task Return_Organizations_When_GetOrganizations()
    {
        // Arrange
        var organizationModels = this._fixture.CreateMany<OrganizationResponse>(5).ToList();
        var queryResult = CollectionResult<OrganizationResponse>.FromValue(organizationModels,5);
        this._mediator.Send(Arg.Any<GetOrganizationsQuery>())
            .Returns(queryResult);
        var request = this._fixture.Build<GetOrganizationsRequest>()
            .Create();

        // Act
        var result = await this._sut.GetOrganizations(request);

        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<OrganizationResponse>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeOfType<PageableResult<OrganizationResponse>>();
    }
    
    [Fact]
    public async Task Return_OrganizationSettings_When_GetOrganizationSettings()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        var organizationSettings = this._fixture.Create<OrganizationSettingsResponse>();
        this._mediator.Send(Arg.Is<GetOrganizationSettingsQuery>(x => x.OrganizationId == organizationId))
            .Returns(organizationSettings);
        var request = this._fixture.Build<GetOrganizationSettingsRequest>()
            .With(x => x.OrganizationId, organizationId)
            .Create();

        // Act
        var result = await this._sut.GetOrganizationSettings(request);

        // Assert
        result.Should().BeOfType<ActionResult<OrganizationSettingsResponse?>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(organizationSettings);
    }

    [Fact]
    public async Task Return_NotFound_When_GetOrganizationSettingsReturnsNull()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Is<GetOrganizationSettingsQuery>(x => x.OrganizationId == organizationId))
            .Returns(null as OrganizationSettingsResponse);
        var request = this._fixture.Build<GetOrganizationSettingsRequest>()
            .With(x => x.OrganizationId, organizationId)
            .Create();

        // Act
        var result = await this._sut.GetOrganizationSettings(request);

        // Assert
        result.Should().BeOfType<ActionResult<OrganizationSettingsResponse?>>();
        result.Result.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async Task Return_OrganizationFee_When_GetOrganizationFeeForYesr()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        var organizationFeeResponse = this._fixture.Create<OrganizationFeeResponse>();
        this._mediator.Send(Arg.Is<GetOrganizationFeeForYearQuery>(x => x.OrganizationId == organizationId))
            .Returns(organizationFeeResponse);
        var request = this._fixture.Build<GetOrganizationFeeForYearRequest>()
            .With(x => x.OrganizationId, organizationId)
            .Create();

        // Act
        var result = await this._sut.GetOrganizationFeeForYear(request);

        // Assert
        result.Should().BeOfType<ActionResult<OrganizationFeeResponse?>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(organizationFeeResponse);
    }

    [Fact]
    public async Task Return_NotFound_When_GetOrganizationFeeForYearReturnsNull()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Is<GetOrganizationFeeForYearQuery>(x => x.OrganizationId == organizationId))
            .Returns(null as OrganizationFeeResponse);
        var request = this._fixture.Build<GetOrganizationFeeForYearRequest>()
            .With(x => x.OrganizationId, organizationId)
            .Create();

        // Act
        var result = await this._sut.GetOrganizationFeeForYear(request);

        // Assert
        result.Should().BeOfType<ActionResult<OrganizationFeeResponse?>>();
        result.Result.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async Task Return_OrganizationFees_When_GetOrganizationFees()
    {
        // Arrange
        var organizationModels = this._fixture.CreateMany<OrganizationFeeResponse>(5).ToList();
        var queryResult = CollectionResult<OrganizationFeeResponse>.FromValue(organizationModels,5);
        this._mediator.Send(Arg.Any<GetOrganizationFeesQuery>())
            .Returns(queryResult);
        var request = this._fixture.Build<GetOrganizationFeesRequest>()
            .Create();

        // Act
        var result = await this._sut.GetOrganizationFees(request);

        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<OrganizationFeeResponse>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeOfType<PageableResult<OrganizationFeeResponse>>();
    }
    
    [Fact]
    public async Task Return_OrganizationId_When_AddMembershipFee()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        var request = this._fixture.Build<AddMembershipFeeRequest>()
            .With(x => x.OrganizationId, organizationId)
            .Create();
        this._mediator.Send(Arg.Is<RequestMembershipFeeCommand>(x => x.OrganizationId == organizationId))
            .Returns(organizationId);
        // Act
        var result = await this._sut.AddMembershipFee(request);

        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(organizationId);
    }
    
    [Fact]
    public async Task Return_OrganizationId_When_UpdateOrganizationSettings()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        var request = this._fixture.Build<UpdateOrganizationSettingsRequest>()
            .With(x => x.OrganizationId, organizationId)
            .Create();
        this._mediator.Send(Arg.Is<UpdateOrganizationSettingsCommand>(x => x.OrganizationId == organizationId))
            .Returns(organizationId);
        // Act
        var result = await this._sut.UpdateSettingsData(request);

        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(organizationId);
    }
    
    [Fact]
    public async Task Return_OrganizationId_When_UpdateOrganizationData()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        var request = this._fixture.Build<UpdateOrganizationDataRequest>()
            .With(x => x.OrganizationId, organizationId)
            .Create();
        this._mediator.Send(Arg.Is<ChangeOrganizationDataCommand>(x => x.OrganizationId == organizationId))
            .Returns(organizationId);
        // Act
        var result = await this._sut.UpdateOrganizationData(request);

        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(organizationId);
    }
    
    [Fact]
    public async Task Return_OrganizationId_When_CreateOrganization()
    {
        // Arrange
        var organizationId = this._fixture.Create<Guid>();
        var request = this._fixture.Build<CreateOrganizationRequest>()
            .Create();
        this._mediator.Send(Arg.Is<CreateOrganizationCommand>(x => x.Name == request.Name))
            .Returns(organizationId);
        // Act
        var result = await this._sut.CreateOrganization(request);

        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(organizationId);
    }
}