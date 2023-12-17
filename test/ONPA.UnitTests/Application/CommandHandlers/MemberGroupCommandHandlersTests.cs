using AutoFixture;
using FluentAssertions;
using NSubstitute;
using ONPA.Common.Domain;
using ONPA.Membership.Api.Application.CommandHandlers;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Domain;

namespace ONPA.UnitTests.Application.CommandHandlers;

public class MemberGroupCommandHandlersTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IAggregateRepository<MembersGroup> _organizationRepository;
    private readonly MembersGroup aggregate;
    
    private readonly Guid tenantId = Guid.NewGuid();

    public MemberGroupCommandHandlersTests()
    {
        _organizationRepository = Substitute.For<IAggregateRepository<MembersGroup>>();
        
        var groupName = this._fixture.Create<string>();
        var groupType = this._fixture.Create<MembersGroupType>();
        aggregate = new MembersGroup(this.tenantId, groupName, groupType);
        
        _organizationRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(aggregate);
    }

    [Fact]
    public async Task CreateMembersGroupCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<CreateMembersGroupCommand>();
        var sut = new CreateMembersGroupCommandHandler(_organizationRepository);
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.AggregateId.Should().NotBeEmpty();
        result.IsFailure.Should().BeFalse();
        result.Errors.Should().BeEmpty();
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task AddMemberToGroupCommandShouldBeHandled()
    {
        // Arrange
        var command = this._fixture.Create<AddMemberToGroupCommand>();
        var sut = new AddMemberToGroupCommandHandler(_organizationRepository);
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.AggregateId.Should().NotBeEmpty();
        result.AggregateId.Should().Be(aggregate.AggregateId);
        result.IsFailure.Should().BeFalse();
        result.Errors.Should().BeEmpty();
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task RemoveMemberFromGroupCommandShouldBeHandled()
    {
        // Arrange
       
        var sut = new RemoveMemberFromGroupCommandHandler(_organizationRepository);
        
        var memberNumber = new CardNumber(this._fixture.Create<int>());
        var memberId = this._fixture.Create<Guid>();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var joinDate = this._fixture.Create<DateTime>();
        
        aggregate.JoinGroup(memberNumber, memberId, firstName, lastName, joinDate);

        var command = this._fixture.Build<RemoveMemberFromGroupCommand>()
            .With(x => x.MemberNumber, memberNumber)
            .Create();
            
        
        // Act
        var result = await sut.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.AggregateId.Should().NotBeEmpty();
        result.AggregateId.Should().Be(aggregate.AggregateId);
        result.IsFailure.Should().BeFalse();
        result.Errors.Should().BeEmpty();
        result.IsValid.Should().BeTrue();
    }
}