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

        aggregate.ModifyContactData(newEmail, newPhone, newAddress);

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

        aggregate.RequestFeePayment(newFee, due, endPeriod);

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

        aggregate.RequestFeePayment(newFee, due, endPeriod);

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

    [Fact]
    public void NotBalancePayment_WhenRegisterPartialPayment()
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
        aggregate.RequestFeePayment(newFee, due, endPeriod);

        var payment = newFee / 2;

        //act
        aggregate.RegisterPaymentFee(aggregate.CurrentFee.Id, payment, DateTime.UtcNow);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.CurrentFee.IsBalanced.Should().BeFalse();

        aggregate.GetStateEvents().Should().HaveCount(3);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberFeePaid>();

        aggregate.GetDomainEvents().Should().HaveCount(3);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberFeePaymentRegisteredDomainEvent>();
    }

    [Fact]
    public void ExtendMembership_WhenRegisterFullPayment()
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
        aggregate.RequestFeePayment(newFee, due, endPeriod);

        var payment = newFee;

        //act
        aggregate.RegisterPaymentFee(aggregate.CurrentFee.Id, payment, DateTime.UtcNow);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.CurrentFee.IsBalanced.Should().BeTrue();

        aggregate.GetStateEvents().Should().HaveCount(3);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberFeePaid>();

        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberFeePaymentRegisteredDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MembershipExtendedDomainEvent>();
    }

    [Fact]
    public void BeReactivated_whenPaidDueFee()
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
        aggregate.RequestFeePayment(newFee, due, endPeriod);

        var payment = newFee;
        aggregate.MembershipExpired(DateTime.UtcNow, this._fixture.Create<string>());

        //act
        aggregate.RegisterPaymentFee(aggregate.CurrentFee.Id, payment, DateTime.UtcNow);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.CurrentFee.IsBalanced.Should().BeTrue();
        aggregate.Status.Should().Be(MembershipStatus.Active);

        aggregate.GetStateEvents().Should().HaveCount(5);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberFeePaid>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MembershipExpired>();

        aggregate.GetDomainEvents().Should().HaveCount(5);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberFeePaymentRegisteredDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MembershipExtendedDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MembershipExpiredDomainEvent>();
    }

    [Fact]
    public void HaveSuspensionDetails_whenSuspended()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        var suspendedUntil = DateTime.UtcNow.AddMonths(6);
        // act

        aggregate.SuspendMember(suspensionJustification, suspensionDate, suspendedUntil, 14);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Suspended);
        aggregate.Suspension.Should().NotBeNull();
        aggregate.Suspension!.SuspensionDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspendedUntil.Should().BeCloseTo(DateTime.UtcNow.AddMonths(6), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspensionJustification.Should().Be(suspensionJustification);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberSuspended>();

        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberSuspendedDomainEvent>();
    }

    [Fact]
    public void AddSuspensionAppealDetail_whenSuspendedMemberAppeal()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        var suspendedUntil = DateTime.UtcNow.AddMonths(6);
        aggregate.SuspendMember(suspensionJustification, suspensionDate, suspendedUntil, 14);
        var appealJustification = this._fixture.Create<string>();

        // act
        aggregate.AppealSuspension(appealJustification, suspensionDate.AddDays(3));

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Suspended);
        aggregate.Suspension.Should().NotBeNull();
        aggregate.Suspension!.SuspensionDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspendedUntil.Should().BeCloseTo(DateTime.UtcNow.AddMonths(6), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspensionJustification.Should().Be(suspensionJustification);
        aggregate.Suspension!.AppealDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(3), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealJustification.Should().Be(appealJustification);

        aggregate.GetStateEvents().Should().HaveCount(3);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberSuspensionAppealReceived>();

        aggregate.GetDomainEvents().Should().HaveCount(3);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberSuspensionAppealReceivedDomainEvent>();
    }

    [Fact]
    public void HaveSuspensionAppealRejected_whenAppealOverDeadline()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        var suspendedUntil = DateTime.UtcNow.AddMonths(6);
        aggregate.SuspendMember(suspensionJustification, suspensionDate, suspendedUntil, 14);
        var appealJustification = this._fixture.Create<string>();

        // act
        aggregate.AppealSuspension(appealJustification, suspensionDate.AddDays(15));

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Suspended);
        aggregate.Suspension.Should().NotBeNull();
        aggregate.Suspension!.SuspensionDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspendedUntil.Should().BeCloseTo(DateTime.UtcNow.AddMonths(6), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspensionJustification.Should().Be(suspensionJustification);
        aggregate.Suspension!.AppealDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(15), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealJustification.Should().Be(appealJustification);
        aggregate.Suspension!.AppealDecisionDate.Should()
            .BeCloseTo(DateTime.UtcNow.AddDays(15), TimeSpan.FromSeconds(2));

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberSuspensionAppealReceived>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<SuspensionAppealDismissed>();

        aggregate.GetDomainEvents().Should().HaveCount(3);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberSuspensionAppealDismissedDomainEvent>();
    }

    [Fact]
    public void HaveDecision_whenSuspendedMemberAppealIsAccepted()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        var suspendedUntil = DateTime.UtcNow.AddMonths(6);
        aggregate.SuspendMember(suspensionJustification, suspensionDate, suspendedUntil, 14);
        var appealJustification = this._fixture.Create<string>();
        aggregate.AppealSuspension(appealJustification, suspensionDate.AddDays(3));

        var decisionDate = DateTime.UtcNow.AddMonths(3);
        var decision = this._fixture.Create<string>();
        //act
        aggregate.AcceptAppealSuspension(decisionDate, decision);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Suspended);
        aggregate.Suspension.Should().NotBeNull();
        aggregate.Suspension!.SuspensionDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspendedUntil.Should().BeCloseTo(DateTime.UtcNow.AddMonths(6), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspensionJustification.Should().Be(suspensionJustification);
        aggregate.Suspension!.AppealDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(3), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealJustification.Should().Be(appealJustification);
        aggregate.Suspension!.AppealDecisionDate.Should()
            .BeCloseTo(DateTime.UtcNow.AddMonths(3), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealDecisionJustification.Should().Be(decision);

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<SuspensionAppealApproved>();

        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberSuspensionAppealApprovedDomainEvent>();
    }

    [Fact]
    public void HaveDecision_whenSuspendedMemberAppealIsDismissed()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        var suspendedUntil = DateTime.UtcNow.AddMonths(6);
        aggregate.SuspendMember(suspensionJustification, suspensionDate, suspendedUntil, 14);
        var appealJustification = this._fixture.Create<string>();
        aggregate.AppealSuspension(appealJustification, suspensionDate.AddDays(3));

        var decisionDate = DateTime.UtcNow.AddMonths(3);
        var decision = this._fixture.Create<string>();
        //act
        aggregate.DismissAppealSuspension(decisionDate, decision);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Suspended);
        aggregate.Suspension.Should().NotBeNull();
        aggregate.Suspension!.SuspensionDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspendedUntil.Should().BeCloseTo(DateTime.UtcNow.AddMonths(6), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.SuspensionJustification.Should().Be(suspensionJustification);
        aggregate.Suspension!.AppealDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(3), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealJustification.Should().Be(appealJustification);
        aggregate.Suspension!.AppealDecisionDate.Should()
            .BeCloseTo(DateTime.UtcNow.AddMonths(3), TimeSpan.FromSeconds(2));
        aggregate.Suspension!.AppealDecisionJustification.Should().Be(decision);

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<SuspensionAppealDismissed>();

        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberSuspensionAppealDismissedDomainEvent>();
    }


    [Fact]
    public void HaveExpelDetails_whenExpelled()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        // act

        aggregate.ExpelMember(suspensionJustification, suspensionDate, 14);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Expelled);
        aggregate.Expel.Should().NotBeNull();
        aggregate.Expel!.ExpelDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Expel!.ExpelJustification.Should().Be(suspensionJustification);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberExpelled>();

        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberExpelledDomainEvent>();
    }

    [Fact]
    public void AddExpelAppealDetail_whenExpelledMemberAppeal()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        aggregate.ExpelMember(suspensionJustification, suspensionDate, 14);
        var appealJustification = this._fixture.Create<string>();

        // act
        aggregate.AppealExpel(appealJustification, suspensionDate.AddDays(3));

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Expelled);
        aggregate.Expel.Should().NotBeNull();
        aggregate.Expel!.ExpelDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Expel!.ExpelJustification.Should().Be(suspensionJustification);
        aggregate.Expel!.AppealDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(3), TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealJustification.Should().Be(appealJustification);

        aggregate.GetStateEvents().Should().HaveCount(3);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberExpelAppealReceived>();

        aggregate.GetDomainEvents().Should().HaveCount(3);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberExpelAppealReceivedDomainEvent>();
    }

    [Fact]
    public void HaveExpelAppealRejected_whenAppealOverDeadline()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        aggregate.ExpelMember(suspensionJustification, suspensionDate, 14);
        var appealJustification = this._fixture.Create<string>();

        // act
        aggregate.AppealExpel(appealJustification, suspensionDate.AddDays(15));

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Expelled);
        aggregate.Expel.Should().NotBeNull();
        aggregate.Expel!.ExpelDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Expel!.ExpelJustification.Should().Be(suspensionJustification);
        aggregate.Expel!.AppealDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(15), TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealJustification.Should().Be(appealJustification);
        aggregate.Expel!.AppealDecisionDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(15), TimeSpan.FromSeconds(2));

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberExpelAppealReceived>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ExpelAppealDismissed>();

        aggregate.GetDomainEvents().Should().HaveCount(3);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberExpelAppealDismissedDomainEvent>();
    }

    [Fact]
    public void HaveDecision_whenExpelledMemberAppealIsAccepted()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        aggregate.ExpelMember(suspensionJustification, suspensionDate, 14);
        var appealJustification = this._fixture.Create<string>();
        aggregate.AppealExpel(appealJustification, suspensionDate.AddDays(3));

        var decisionDate = DateTime.UtcNow.AddMonths(3);
        var decision = this._fixture.Create<string>();
        //act
        aggregate.AcceptAppealExpel(decisionDate, decision);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Expelled);
        aggregate.Expel.Should().NotBeNull();
        aggregate.Expel!.ExpelDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Expel!.ExpelJustification.Should().Be(suspensionJustification);
        aggregate.Expel!.AppealDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(3), TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealJustification.Should().Be(appealJustification);
        aggregate.Expel!.AppealDecisionDate.Should().BeCloseTo(DateTime.UtcNow.AddMonths(3), TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealDecisionJustification.Should().Be(decision);

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ExpelAppealApproved>();

        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberExpelAppealApprovedDomainEvent>();
    }

    [Fact]
    public void HaveDecision_whenExpelledMemberAppealIsDismissed()
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

        var suspensionJustification = this._fixture.Create<string>();
        var suspensionDate = DateTime.UtcNow;
        aggregate.ExpelMember(suspensionJustification, suspensionDate, 14);
        var appealJustification = this._fixture.Create<string>();
        aggregate.AppealExpel(appealJustification, suspensionDate.AddDays(3));

        var decisionDate = DateTime.UtcNow.AddMonths(3);
        var decision = this._fixture.Create<string>();
        //act
        aggregate.DismissAppealExpel(decisionDate, decision);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.Status.Should().Be(MembershipStatus.Deleted);
        aggregate.Expel.Should().NotBeNull();
        aggregate.Expel!.ExpelDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealDeadline.Should().BeCloseTo(DateTime.UtcNow.AddDays(14), TimeSpan.FromSeconds(2));
        aggregate.Expel!.ExpelJustification.Should().Be(suspensionJustification);
        aggregate.Expel!.AppealDate.Should().BeCloseTo(DateTime.UtcNow.AddDays(3), TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealJustification.Should().Be(appealJustification);
        aggregate.Expel!.AppealDecisionDate.Should().BeCloseTo(DateTime.UtcNow.AddMonths(3), TimeSpan.FromSeconds(2));
        aggregate.Expel!.AppealDecisionJustification.Should().Be(decision);

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<ExpelAppealDismissed>();

        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberExpelAppealDismissedDomainEvent>();
    }
    
    [Fact]
    public void BeAbleToLeaveWhenActive()
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

        aggregate.LeaveOrganization(DateTime.UtcNow);

        // assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);
        aggregate.Status.Should().Be(MembershipStatus.Leaved);
        aggregate.TerminationDate.Should().NotBeNull().And.BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        
        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<MemberLeft>();

        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<MemberLeftDomainEvent>();
    }
}