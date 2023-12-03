using AutoFixture;
using FluentAssertions;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Domain;
using PLUG.System.Membership.StateEvents;

namespace PLUG.System.Apply.UnitTests.Domain;

public class MembersGroupShould
{
    private readonly IFixture _fixture;

    public MembersGroupShould()
    {
        this._fixture = new Fixture();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void BeCreated(int type)
    {
        // arrange
        var groupName = this._fixture.Create<string>();
        var groupType = Enumeration.FromValue<MembersGroupType>(type);
        
        // act
        var aggregate = new MembersGroup(groupName, groupType);
        
        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GroupName.Should().Be(groupName);
        aggregate.GroupType.Should().Be(groupType);
        aggregate.GroupMembers.Should().BeEmpty();

        aggregate.GetStateEvents().Should().HaveCount(1);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<GroupCreated>();
    }
    
    [Fact]
    public void AddMemberToGroup()
    {
        // arrange
        var groupName = this._fixture.Create<string>();
        var groupType = MembersGroupType.CustomGroup;
        var aggregate = new MembersGroup(groupName, groupType);

        var memberNumber = new CardNumber(this._fixture.Create<int>());
        var memberId = this._fixture.Create<Guid>();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var joinDate = this._fixture.Create<DateTime>();
        
        // act
        aggregate.JoinGroup(memberNumber,memberId,firstName,lastName,joinDate);
        
        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GroupName.Should().Be(groupName);
        aggregate.GroupType.Should().Be(groupType);
        aggregate.GroupMembers.Should().HaveCount(1);
        var member = aggregate.GroupMembers.First();
        member.MemberId.Should().Be(memberId);
        member.MemberNumber.Should().Be(memberNumber);
        member.FirstName.Should().Be(firstName);
        member.LastName.Should().Be(lastName);
        member.JoinDate.Should().Be(joinDate);
        member.IsActive.Should().BeTrue();

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberJoinedGroup>();
    }
    
    [Fact]
    public void ThrowWhenAddingNotUniqueMemberToGroup()
    {
        // arrange
        var groupName = this._fixture.Create<string>();
        var groupType = MembersGroupType.CustomGroup;
        var aggregate = new MembersGroup(groupName, groupType);

        var memberNumber = new CardNumber(this._fixture.Create<int>());
        var memberId = this._fixture.Create<Guid>();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var joinDate = this._fixture.Create<DateTime>();
        aggregate.JoinGroup(memberNumber,memberId,firstName,lastName,joinDate);
        
        // act
        var action = ()=> aggregate.JoinGroup(memberNumber,memberId,firstName,lastName,joinDate);
        
        // assert
        action.Should().Throw<InvalidDomainOperationException>();
        aggregate.GroupMembers.Should().HaveCount(1);
    }
    
    [Fact]
    public void RemoveMemberFromGroup()
    {
        // arrange
        var groupName = this._fixture.Create<string>();
        var groupType = MembersGroupType.CustomGroup;
        var aggregate = new MembersGroup(groupName, groupType);

        var memberNumber = new CardNumber(this._fixture.Create<int>());
        var memberId = this._fixture.Create<Guid>();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var joinDate = this._fixture.Create<DateTime>();
        aggregate.JoinGroup(memberNumber,memberId,firstName,lastName,joinDate);
        
        // act
        aggregate.RemoveFromGroup(memberNumber, DateTime.UtcNow.AddDays(-6));
        
        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.GroupName.Should().Be(groupName);
        aggregate.GroupType.Should().Be(groupType);
        aggregate.GroupMembers.Should().HaveCount(1);
        var member = aggregate.GroupMembers.First();
        member.MemberId.Should().Be(memberId);
        member.MemberNumber.Should().Be(memberNumber);
        member.FirstName.Should().Be(firstName);
        member.LastName.Should().Be(lastName);
        member.JoinDate.Should().Be(joinDate);
        member.IsActive.Should().BeFalse();

        aggregate.GetStateEvents().Should().HaveCount(3);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberLeftGroup>();
    }
    
    [Fact]
    public void ThrowWhenRemovingNonGroupMember()
    {
        // arrange
        var groupName = this._fixture.Create<string>();
        var groupType = MembersGroupType.CustomGroup;
        var aggregate = new MembersGroup(groupName, groupType);

        var memberNumber = new CardNumber(this._fixture.Create<int>());
        var memberId = this._fixture.Create<Guid>();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var joinDate = this._fixture.Create<DateTime>();
        aggregate.JoinGroup(memberNumber,memberId,firstName,lastName,joinDate);
        CardNumber another = (CardNumber)this._fixture.Create<int>();
        
        // act
        var action = ()=> aggregate.RemoveFromGroup(another,joinDate);
        
        // assert
        action.Should().Throw<EntityNotFoundException>();
        aggregate.GroupMembers.Should().HaveCount(1);
    }
    
    [Fact]
    public void RestoreGroup()
    {
        // arrange
        var groupName = this._fixture.Create<string>();
        var groupType = MembersGroupType.CustomGroup;
        var aggregate = new MembersGroup(groupName, groupType);

        var memberNumber = new CardNumber(this._fixture.Create<int>());
        var memberId = this._fixture.Create<Guid>();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var joinDate = this._fixture.Create<DateTime>();
        aggregate.JoinGroup(memberNumber,memberId,firstName,lastName,joinDate);
        aggregate.RemoveFromGroup(memberNumber,DateTime.UtcNow);
        
        var events = new List<IStateEvent>();
        
        events.AddRange(aggregate.GetStateEvents());
        aggregate.ClearChanges();
        aggregate.ClearDomainEvents();
        
        // act
        var newAggregate = new MembersGroup(aggregate.AggregateId, events);
        
        // assert
        newAggregate.Should().BeEquivalentTo(aggregate);

    }
}