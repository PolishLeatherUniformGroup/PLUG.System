using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using ONPA.Common.Application;
using ONPA.Organizations.Api.Application.Queries;
using ONPA.Organizations.Api.Application.QueryHandlers;
using ONPA.Organizations.Api.Maps;
using ONPA.Organizations.Contract.Responses;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.UnitTests.Application.QueryHandlers;

public class OrganizationQueryHandlersTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IMapper _mapper;
    
    private readonly IReadOnlyRepository<Organization> _organizationRepository;
    private readonly IReadOnlyRepository<OrganizationSettings> _organizationSettingsRepository;
    private readonly IReadOnlyRepository<OrganizationFee> _organizationFeeRepository;

    public OrganizationQueryHandlersTests()
    {
        this._organizationRepository = Substitute.For<IReadOnlyRepository<Organization>>();
        this._organizationSettingsRepository = Substitute.For<IReadOnlyRepository<OrganizationSettings>>();
        this._organizationFeeRepository = Substitute.For<IReadOnlyRepository<OrganizationFee>>();
        this._mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrganizationsMaps>();
            cfg.ShouldUseConstructor = ci => ci.IsPublic;
        }).CreateMapper();
    }
    
    [Fact]
    public async Task GetOrganizationByIdQueryHandler_Returns_Organization()
    {
        // Arrange
        var organization = _fixture.Create<Organization>();
        var query = new GetOrganizationQuery(organization.Id);
        this._organizationRepository.ReadSingleById(organization.Id, Arg.Any<CancellationToken>()).Returns(organization);
        var queryHandler = new GetOrganizationQueryHandler(this._organizationRepository, _mapper);
        
        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OrganizationResponse>();
        result.Should().BeEquivalentTo(organization, options => options.ExcludingMissingMembers());
    }
    
    [Fact]
    public async Task GetOrganizationByIdQueryHandler_Returns_Null()
    {
        // Arrange
        var organization = _fixture.Create<Organization>();
        var query = new GetOrganizationQuery(organization.Id);
        this._organizationRepository.ReadSingleById(organization.Id, Arg.Any<CancellationToken>()).Returns(null as Organization);
        var queryHandler = new GetOrganizationQueryHandler(this._organizationRepository, _mapper);
        
        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeNull();
    
    }
    
    [Fact]
    public async Task GetOrganizationSettingsQueryHandler_Returns_OrganizationSettings()
    {
        // Arrange
        var organization = _fixture.Create<OrganizationSettings>();
        var query = new GetOrganizationSettingsQuery(organization.OrganizationId);
        this._organizationSettingsRepository.ReadSingleById(organization.OrganizationId, Arg.Any<CancellationToken>()).Returns(organization);
        var queryHandler = new GetOrganizationSettingsQueryHandler(this._organizationSettingsRepository, _mapper);
        
        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OrganizationSettingsResponse>();
        result.Should().BeEquivalentTo(organization, options => options.ExcludingMissingMembers());
    }
    
    [Fact]
    public async Task GetOrganizationSettingsQueryHandler_Returns_Null()
    {
        // Arrange
        var organization = _fixture.Create<OrganizationSettings>();
        var query = new GetOrganizationSettingsQuery(organization.OrganizationId);
        this._organizationSettingsRepository.ReadSingleById(organization.OrganizationId, Arg.Any<CancellationToken>()).Returns(null as OrganizationSettings);
        var queryHandler = new GetOrganizationSettingsQueryHandler(this._organizationSettingsRepository, _mapper);
        
        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeNull();
    
    }
    
    [Fact]
    public async Task GetOrganizationFeeQueryHandler_Returns_OrganizationFee()
    {
        // Arrange
        var organization = _fixture.Create<OrganizationFee>();
        var query = new GetOrganizationFeesQuery(organization.OrganizationId,0,10);
        this._organizationFeeRepository.ReadSingleById(organization.OrganizationId, Arg.Any<CancellationToken>()).Returns(organization);
        var queryHandler = new GetOrganizationFeesQueryHandler(this._organizationFeeRepository, _mapper);
        
        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CollectionResult<OrganizationFeeResponse>>();
    }
    
    [Fact]
    public async Task GetOrganizationFeeForYearQueryHandler_Returns_OrganizationFee()
    {
        // Arrange
        var organization = _fixture.Create<OrganizationFee>();
        var query = new GetOrganizationFeeForYearQuery(organization.OrganizationId,organization.Year);
        this._organizationFeeRepository.ManyByFilter(Arg.Any<Expression<Func<OrganizationFee,bool>>>(), Arg.Any<int>(),Arg.Any<int>()).Returns(new []{organization});
        var queryHandler = new GetOrganizationFeeForYearQueryHandler(this._organizationFeeRepository, _mapper);
        
        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OrganizationFeeResponse>();
        result.Should().BeEquivalentTo(organization, options => options.ExcludingMissingMembers());

    }
    
    [Fact]
    public async Task GetOrganizationFeeForYearQueryHandler_Returns_Null()
    {
        // Arrange
        var organization = _fixture.Create<OrganizationFee>();
        var query = new GetOrganizationFeeForYearQuery(organization.OrganizationId,organization.Year);
        this._organizationFeeRepository.ManyByFilter(Arg.Any<Expression<Func<OrganizationFee,bool>>>(), Arg.Any<int>(),Arg.Any<int>()).Returns(Enumerable.Empty<OrganizationFee>());
        var queryHandler = new GetOrganizationFeeForYearQueryHandler(this._organizationFeeRepository, _mapper);
        
        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeNull();
    
    }
    
    [Fact]
    public async Task GetOrganizationsQueryHandler_Returns_Organizations()
    {
        // Arrange
        var organization = _fixture.Create<Organization>();
        var query = new GetOrganizationsQuery(0,10);
        this._organizationRepository.ManyByFilter(Arg.Any<Expression<Func<Organization,bool>>>(), Arg.Any<int>(),Arg.Any<int>()).Returns(new []{organization});
        var queryHandler = new GetOrganizationsQueryHandler(this._organizationRepository, _mapper);
        
        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CollectionResult<OrganizationResponse>>();
    }
}