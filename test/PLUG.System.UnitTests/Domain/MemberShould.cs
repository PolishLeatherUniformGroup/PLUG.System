using AutoFixture;
using FluentAssertions;
using PLUG.System.Membership.Domain;
using PLUG.System.Membership.DomainEvents;
using PLUG.System.Membership.StateEvents;
using PLUG.System.SharedDomain;
using PLUG.System.SharedDomain.Helpers;

namespace PLUG.System.Apply.UnitTests.Domain;

public class MemberShould
{
    private readonly IFixture _fixture;

    public MemberShould()
    {
        this._fixture = new Fixture();
    }

    [Fact]
    public void BeCreatedAndRaiseEvents()
    {
        // arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var number = new CardNumber(this._fixture.Create<int>());
        var join = this._fixture.Create<DateTime>();
        var paidFee = new Money(this._fixture.Create<decimal>());
        // act 
        var aggregate = new Member(number, firstName, lastName, email, phone, address, join, paidFee);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.MemberNumber.Should().Be(number);
        aggregate.FirstName.Should().Be(firstName);
        aggregate.LastName.Should().Be(lastName);
        aggregate.Email.Should().Be(email);
        aggregate.Phone.Should().Be(phone);
        aggregate.Address.Should().Be(address);
        aggregate.JoinDate.Should().Be(join);
        aggregate.Status.Should().Be(MembershipStatus.Active);
        aggregate.CurrentFee.Should().NotBeNull();
        aggregate.Expel.Should().BeNull();
        aggregate.Suspension.Should().BeNull();
        aggregate.MembershipType.Should().Be(MembershipType.Regular);
        aggregate.MembershipValidUntil.Should().Be(join.ToYearEnd());

        aggregate.GetStateEvents().Should().HaveCount(1);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberCreated>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberJoinedDomainEvent>();
    }
    
    [Fact]
    public void ModifyContactDataWhenActive()
    {
        // arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var number = new CardNumber(this._fixture.Create<int>());
        var join = this._fixture.Create<DateTime>();
        var paidFee = new Money(this._fixture.Create<decimal>());
        var newEmail = this._fixture.Create<string>();
        var newPhone = this._fixture.Create<string>();
        var newAddress = this._fixture.Create<string>();
        var aggregate = new Member(number, firstName, lastName, email, phone, address, join, paidFee);
        
        // act
        
        aggregate.ModifyContactData(newEmail,newPhone,newAddress);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Email.Should().Be(newEmail);
        aggregate.Phone.Should().Be(newPhone);
        aggregate.Address.Should().Be(newAddress);


        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberContactDataModified>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberJoinedDomainEvent>();
    }
    
    [Fact]
    public void BeMadeHonoraryWhenActive()
    {
        // arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var number = new CardNumber(this._fixture.Create<int>());
        var join = this._fixture.Create<DateTime>();
        var paidFee = new Money(this._fixture.Create<decimal>());
     var aggregate = new Member(number, firstName, lastName, email, phone, address, join, paidFee);
        
        // act
        
        aggregate.MakeHonoraryMember();

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.MembershipType.Should().Be(MembershipType.Honorary);


        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberTypeChanged>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberJoinedDomainEvent>();
    }
    
    [Fact]
    public void BeMadeRegularWhenActive()
    {
        // arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var number = new CardNumber(this._fixture.Create<int>());
        var join = this._fixture.Create<DateTime>();
        var paidFee = new Money(this._fixture.Create<decimal>());
        var aggregate = new Member(number, firstName, lastName, email, phone, address, join, paidFee);
        aggregate.MakeHonoraryMember();
        
        // act
        
        aggregate.MakeRegularMember();

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.MembershipType.Should().Be(MembershipType.Regular);


        aggregate.GetStateEvents().Should().HaveCount(3);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberTypeChanged>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberJoinedDomainEvent>();
    }
    
    [Fact]
    public void DoNothingWhenRequestedFeePaymentForPaidYear()
    {
        // arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var number = new CardNumber(this._fixture.Create<int>());
        var join = this._fixture.Create<DateTime>();
        var paidFee = new Money(this._fixture.Create<decimal>());
        var aggregate = new Member(number, firstName, lastName, email, phone, address, join, paidFee);
        var newFee = new Money(this._fixture.Create<decimal>());
        var due = join.AddMonths(3);
        var endPeriod = join.ToYearEnd();
        
        // act
        
        aggregate.RequestFeePayment(newFee,due,endPeriod);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.MembershipType.Should().Be(MembershipType.Regular);

        aggregate.GetStateEvents().Should().HaveCount(1);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberCreated>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberJoinedDomainEvent>();
    }
    
    [Fact]
    public void AddFeeWhenRequestedFeePaymentForNotPaidYear()
    {
        // arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var number = new CardNumber(this._fixture.Create<int>());
        var join = this._fixture.Create<DateTime>();
        var paidFee = new Money(this._fixture.Create<decimal>());
        var aggregate = new Member(number, firstName, lastName, email, phone, address, join, paidFee);
        var newFee = new Money(this._fixture.Create<decimal>());
        var due = join.AddMonths(3);
        var endPeriod = join.ToYearEnd().AddYears(1);
        
        // act
        
        aggregate.RequestFeePayment(newFee,due,endPeriod);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.MembershipType.Should().Be(MembershipType.Regular);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberFeeRequested>();

        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberFeePaymentRequestedDomainEvent>();
    }
}