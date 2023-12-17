using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using ONPA.Common.Application;
using ONPA.Common.Queries;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Api.Application.QueryHandlers;
using ONPA.Membership.Api.Maps;
using ONPA.Membership.Contract.Responses;
using ONPA.Membership.Infrastructure.ReadModel;
using MemberFee = ONPA.Membership.Infrastructure.ReadModel.MemberFee;

namespace ONPA.UnitTests.Application.QueryHandlers;

public class MembersQueryHandlersTest
{
    private readonly IFixture _fixture;
    private readonly IReadOnlyRepository<Member> _memberRepository;
    private readonly IReadOnlyRepository<MemberFee> _memberFeeRepository;
    private readonly IReadOnlyRepository<MemberSuspension> _memberSuspensionRepository;
    private readonly IReadOnlyRepository<MemberExpel> _memberExpelRepository;
    private readonly IMapper _mapper;
    private readonly Guid _tenantId = Guid.NewGuid();
    public MembersQueryHandlersTest()
    {
        _fixture = new Fixture();
        _memberRepository = Substitute.For<IReadOnlyRepository<Member>>();
        _memberFeeRepository = Substitute.For<IReadOnlyRepository<MemberFee>>();
        _memberSuspensionRepository = Substitute.For<IReadOnlyRepository<MemberSuspension>>();
        _memberExpelRepository = Substitute.For<IReadOnlyRepository<MemberExpel>>();
        this._mapper =  new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MembershipMaps>();
            cfg.ShouldUseConstructor = ci => ci.IsPublic;
        }).CreateMapper();
    }
    
    [Fact]
    public async Task GetMemberByIdQueryHandler_ReturnsMember()
    {
        // Arrange
        var member = _fixture.Create<Member>();
        var query = new GetMemberByIdQuery(this._tenantId,member.Id);
        _memberRepository.ReadSingleById(member.Id, Arg.Any<CancellationToken>()).Returns(member);
        var handler = new GetMemberByIdQueryHandler(_memberRepository, _mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<MemberResult>();
    }
    
    [Fact]
    public async Task GetMemberByIdQueryHandler_Returns_Null()
    {
        // Arrange
        var member = _fixture.Create<Member>();
        var query = new GetMemberByIdQuery(this._tenantId,member.Id);
        _memberRepository.ReadSingleById(member.Id, Arg.Any<CancellationToken>()).Returns(null as Member);
        var handler = new GetMemberByIdQueryHandler(_memberRepository, _mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeNull();
    }
    
    [Fact]
    public async Task GetMembersQueryHandler_ReturnsMembers()
    {
        // Arrange
        var members = _fixture.CreateMany<Member>();
        var query = this._fixture.Create<GetMembersByStatusQuery>();
        _memberRepository.ManyByFilter(Arg.Any<Expression<Func<Member, bool>>>(),Arg.Any<int>(),Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(members);
        var handler = new GetMembersByStatusQueryHandler(_memberRepository,_mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CollectionResult<MemberResult>>();
    }
    
    [Fact]
    public async Task GetMemberFeesQueryHandler_ReturnsMemberFees()
    {
        // Arrange
        var memberFees = _fixture.CreateMany<MemberFee>();
        var query = this._fixture.Create<GetMemberFeesQuery>();
        _memberFeeRepository.ManyByFilter(Arg.Any<Expression<Func<MemberFee, bool>>>(),Arg.Any<int>(),Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(memberFees);
        var handler = new GetMemberFeesQueryHandler(_memberFeeRepository,_mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CollectionResult<MemberFeeResult>>();
    }
    
    [Fact]
    public async Task GetMemberSuspenstionsQueryHandler_ReturnsMemberSuspensions()
    {
        // Arrange
        var memberSuspensions = _fixture.CreateMany<MemberSuspension>();
        var query = this._fixture.Create<GetMemberSuspensionsQuery>();
        _memberSuspensionRepository.ManyByFilter(Arg.Any<Expression<Func<MemberSuspension, bool>>>(),Arg.Any<int>(),Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(memberSuspensions);
        var handler = new GetMemberSuspensionsQueryHandler(_memberSuspensionRepository,_mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CollectionResult<MemberSuspensionResult>>();
    }
    
    [Fact]
    public async Task GetMemberExpelsQueryHandler_ReturnsMemberExpels()
    {
        // Arrange
        var memberExpels = _fixture.CreateMany<MemberExpel>();
        var query = this._fixture.Create<GetMemberExpelsQuery>();
        _memberExpelRepository.ManyByFilter(Arg.Any<Expression<Func<MemberExpel, bool>>>(),Arg.Any<int>(),Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(memberExpels);
        var handler = new GetMemberExpelsQueryHandler(_memberExpelRepository,_mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CollectionResult<MemberExpelResult>>();
    }
}