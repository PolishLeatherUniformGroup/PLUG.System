using AutoFixture;
using FluentAssertions;
using ONPA.Gatherings.Domain;
using ONPA.Gatherings.DomainEvents;
using ONPA.Gatherings.StateEvents;
using PLUG.System.SharedDomain;

namespace ONPA.UnitTests.Domain;

public class PublicGatheringShould
{
    private readonly IFixture _fixture;

    public PublicGatheringShould()
    {
        this._fixture = new Fixture();
    }

    [Fact]
    public void CreatePublicGathering()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();

        // Act
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Name.Should().Be(name);
        aggregate.Description.Should().Be(description);
        aggregate.Regulations.Should().Be(regulations);
        aggregate.ScheduledStart.Should().Be(scheduledStart);
        aggregate.PlannedCapacity.Should().Be(plannedCapacity);
        aggregate.PricePerPerson.Should().Be(pricePerPerson);
        aggregate.PublishDate.Should().Be(publishDate);
        aggregate.EnrollmentDeadline.Should().Be(enrollmentDeadline);
        aggregate.Status.Should().Be(PublicGatheringStatus.Draft);
        aggregate.IsCostFree.Should().BeFalse();
        aggregate.IsAvailable.Should().BeFalse();
        aggregate.Registrations.Should().BeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.GetStateEvents().Should().HaveCount(1);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
    }

    [Fact]
    public void ChangeDescriptions()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newName = this._fixture.Create<string>();
        var newDescription = this._fixture.Create<string>();

        // Act
        aggregate.ModifyDescriptions(newName, newDescription, regulations);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Name.Should().Be(newName);
        aggregate.Description.Should().Be(newDescription);
        aggregate.Regulations.Should().Be(regulations);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringDescriptionModified>();
    }

    [Fact]
    public void ModifyCapacity()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newCapacity = this._fixture.Create<int>();

        // Act
        aggregate.ModifyCapacity(newCapacity);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.PlannedCapacity.Should().Be(newCapacity);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCapacityChanged>();
    }

    [Fact]
    public void ModifyPrice()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newPrice = new Money(this._fixture.Create<decimal>());

        // Act
        aggregate.ModifyPrice(newPrice);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.PricePerPerson.Should().Be(newPrice);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringPriceChanged>();
    }

    [Fact]
    public void ModifySchedule()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newScheduledStart = this._fixture.Create<DateTime>();
        var newPublishDate = this._fixture.Create<DateTime>();
        var newEnrollmentDeadline = this._fixture.Create<DateTime>();

        // Act
        aggregate.ModifySchedule(newScheduledStart, newPublishDate, newEnrollmentDeadline);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.ScheduledStart.Should().Be(newScheduledStart);
        aggregate.PublishDate.Should().Be(newPublishDate);
        aggregate.EnrollmentDeadline.Should().Be(newEnrollmentDeadline);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringScheduleChanged>();
    }

    [Fact]
    public void Archive()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);

        // Act
        aggregate.Archive();

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(PublicGatheringStatus.Archived);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringStatusChanged>();
    }

    [Fact]
    public void Accept()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        
        // Act
        aggregate.Accept();
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(PublicGatheringStatus.ReadyToPublish);
        
        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringStatusChanged>();
    }

    [Fact]
    public void Publish()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        
        // Act
        aggregate.Publish();
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(PublicGatheringStatus.Published);
        
        aggregate.GetStateEvents().Should().HaveCount(3);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringStatusChanged>();
    }
    
    

    [Fact]
    public void Enroll()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        aggregate.Publish();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var participants = new List<Participant>
        {
            new(firstName, lastName, email)
        };
        var registrationDate = this._fixture.Create<DateTime>();
        var bookedPlaces = 1;
        
        // Act
        aggregate.Enroll(registrationDate, bookedPlaces, firstName, lastName, email, participants);
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(PublicGatheringStatus.Published);
        aggregate.Registrations.Should().HaveCount(1);
        aggregate.Registrations.First().Should().NotBeNull();
        aggregate.Registrations.First().RegistrationDate.Should().Be(registrationDate);
        aggregate.Registrations.First().PlacesBooked.Should().Be(bookedPlaces);
        aggregate.Registrations.First().FirstName.Should().Be(firstName);
        aggregate.Registrations.First().LastName.Should().Be(lastName);
        aggregate.Registrations.First().Email.Should().Be(email);
        aggregate.Registrations.First().RequiredPayment.Should().BeEquivalentTo(pricePerPerson);
        aggregate.Registrations.First().PaidAmount.Should().BeNull();
        aggregate.Registrations.First().PaidDate.Should().BeNull();
        aggregate.Registrations.First().CancellationDate.Should().BeNull();
        aggregate.Registrations.First().RefundableAmount.Should().BeNull();
        aggregate.Registrations.First().RefundedAmount.Should().BeNull();
        aggregate.Registrations.First().RefundDate.Should().BeNull();
        aggregate.Registrations.First().IsRefunded.Should().BeFalse();
        aggregate.Registrations.First().Participants.Should().HaveCount(1);
        aggregate.Registrations.First().Participants.First().Should().NotBeNull();
        aggregate.Registrations.First().Participants.First().FirstName.Should().Be(firstName);
        aggregate.Registrations.First().Participants.First().LastName.Should().Be(lastName);
        aggregate.Registrations.First().Participants.First().Email.Should().Be(email);
        
        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToPublicGathering>();
        
        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToPublicGatheringDomainEvent>();
    }

    [Fact]
    public void RegisterPaymentForEnrollment()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        aggregate.Publish();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var participants = new List<Participant>
        {
            new(firstName, lastName, email)
        };
        var registrationDate = this._fixture.Create<DateTime>();
        var bookedPlaces = 1;
        aggregate.Enroll(registrationDate, bookedPlaces, firstName, lastName, email, participants);
        
        var enrollment = aggregate.Registrations.First();
        var paidDate = this._fixture.Create<DateTime>();
        var paidAmount = pricePerPerson;
        
        // Act
        aggregate.RegisterEnrollmentPayment(enrollment.Id, paidDate, paidAmount);
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(PublicGatheringStatus.Published);
        aggregate.Registrations.Should().HaveCount(1);
        aggregate.Registrations.First().Should().NotBeNull();
        aggregate.Registrations.First().RegistrationDate.Should().Be(registrationDate);
        aggregate.Registrations.First().PlacesBooked.Should().Be(bookedPlaces);
        aggregate.Registrations.First().FirstName.Should().Be(firstName);
        aggregate.Registrations.First().LastName.Should().Be(lastName);
        aggregate.Registrations.First().Email.Should().Be(email);
        aggregate.Registrations.First().RequiredPayment.Should().BeEquivalentTo(pricePerPerson);
        aggregate.Registrations.First().PaidAmount.Should().BeEquivalentTo(paidAmount);
        aggregate.Registrations.First().PaidDate.Should().Be(paidDate);
        aggregate.Registrations.First().CancellationDate.Should().BeNull();
        aggregate.Registrations.First().RefundableAmount.Should().BeNull();
        aggregate.Registrations.First().RefundedAmount.Should().BeNull();
        aggregate.Registrations.First().RefundDate.Should().BeNull();
        aggregate.Registrations.First().IsRefunded.Should().BeFalse();
        aggregate.Registrations.First().Participants.Should().HaveCount(1);
        aggregate.Registrations.First().Participants.First().Should().NotBeNull();
        aggregate.Registrations.First().Participants.First().FirstName.Should().Be(firstName);
        aggregate.Registrations.First().Participants.First().LastName.Should().Be(lastName);
        aggregate.Registrations.First().Participants.First().Email.Should().Be(email);
        
        aggregate.GetStateEvents().Should().HaveCount(5);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToPublicGathering>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegistered>();
        
        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToPublicGatheringDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegisteredDomainEvent>();
    }

    [Fact]
    public void CancelEnrollment()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        aggregate.Publish();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var participants = new List<Participant>
        {
            new(firstName, lastName, email)
        };
        var registrationDate = this._fixture.Create<DateTime>();
        var bookedPlaces = 1;
        aggregate.Enroll(registrationDate, bookedPlaces, firstName, lastName, email, participants);
        
        var enrollment = aggregate.Registrations.First();
        var paidDate = this._fixture.Create<DateTime>();
        var paidAmount = pricePerPerson;
        aggregate.RegisterEnrollmentPayment(enrollment.Id, paidDate, paidAmount);
        
        var cancellationDate = this._fixture.Create<DateTime>();
        var refundableAmount = pricePerPerson;
        
        // Act
        aggregate.CancelEnrollment(enrollment.Id, cancellationDate, refundableAmount);
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(PublicGatheringStatus.Published);
        aggregate.Registrations.Should().HaveCount(1);
        aggregate.Registrations.First().Should().NotBeNull();
        aggregate.Registrations.First().RegistrationDate.Should().Be(registrationDate);
        aggregate.Registrations.First().PlacesBooked.Should().Be(bookedPlaces);
        aggregate.Registrations.First().FirstName.Should().Be(firstName);
        aggregate.Registrations.First().LastName.Should().Be(lastName);
        aggregate.Registrations.First().Email.Should().Be(email);
        aggregate.Registrations.First().RequiredPayment.Should().BeEquivalentTo(pricePerPerson);
        aggregate.Registrations.First().PaidAmount.Should().BeEquivalentTo(paidAmount);
        aggregate.Registrations.First().PaidDate.Should().Be(paidDate);
        aggregate.Registrations.First().CancellationDate.Should().Be(cancellationDate);
        aggregate.Registrations.First().RefundableAmount.Should().Be(refundableAmount);
        aggregate.Registrations.First().RefundedAmount.Should().BeNull();
        aggregate.Registrations.First().RefundDate.Should().BeNull();
        aggregate.Registrations.First().IsRefunded.Should().BeFalse();
        aggregate.Registrations.First().Participants.Should().HaveCount(1);
        aggregate.Registrations.First().Participants.First().Should().NotBeNull();
        aggregate.Registrations.First().Participants.First().FirstName.Should().Be(firstName);
        aggregate.Registrations.First().Participants.First().LastName.Should().Be(lastName);
        aggregate.Registrations.First().Participants.First().Email.Should().Be(email);
        
        aggregate.GetStateEvents().Should().HaveCount(6);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToPublicGathering>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegistered>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentCancelled>();
        
        aggregate.GetDomainEvents().Should().HaveCount(3);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToPublicGatheringDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegisteredDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentCancelledDomainEvent>();
    }

    [Fact]
    public void RefundCanceledEnrollment()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        aggregate.Publish();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var participants = new List<Participant>
        {
            new(firstName, lastName, email)
        };
        var registrationDate = this._fixture.Create<DateTime>();
        var bookedPlaces = 1;
        aggregate.Enroll(registrationDate, bookedPlaces, firstName, lastName, email, participants);
        
        var enrollment = aggregate.Registrations.First();
        var paidDate = this._fixture.Create<DateTime>();
        var paidAmount = pricePerPerson;
        aggregate.RegisterEnrollmentPayment(enrollment.Id, paidDate, paidAmount);
        
        var cancellationDate = this._fixture.Create<DateTime>();
        var refundableAmount = pricePerPerson;
        aggregate.CancelEnrollment(enrollment.Id, cancellationDate, refundableAmount);
        
        var refundDate = this._fixture.Create<DateTime>();
        var refundAmount = pricePerPerson;
        
        // Act
        aggregate.RefundEnrollment(enrollment.Id, refundDate, refundAmount);
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(PublicGatheringStatus.Published);
        aggregate.Registrations.Should().HaveCount(1);
        aggregate.Registrations.First().Should().NotBeNull();
        aggregate.Registrations.First().RegistrationDate.Should().Be(registrationDate);
        aggregate.Registrations.First().PlacesBooked.Should().Be(bookedPlaces);
        aggregate.Registrations.First().FirstName.Should().Be(firstName);  
        aggregate.Registrations.First().LastName.Should().Be(lastName);
        aggregate.Registrations.First().Email.Should().Be(email);
        aggregate.Registrations.First().RequiredPayment.Should().BeEquivalentTo(pricePerPerson);
        aggregate.Registrations.First().PaidAmount.Should().BeEquivalentTo(paidAmount);
        aggregate.Registrations.First().PaidDate.Should().Be(paidDate);
        aggregate.Registrations.First().CancellationDate.Should().Be(cancellationDate);
        aggregate.Registrations.First().RefundableAmount.Should().Be(refundableAmount);
        aggregate.Registrations.First().RefundedAmount.Should().Be(refundAmount);
        aggregate.Registrations.First().RefundDate.Should().Be(refundDate);
        aggregate.Registrations.First().IsRefunded.Should().BeTrue();
        
        aggregate.GetStateEvents().Should().HaveCount(7);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToPublicGathering>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegistered>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentCancelled>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentRefunded>();
        
        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToPublicGatheringDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegisteredDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentCancelledDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentRefundedDomainEvent>();
    }

    [Fact]
    public void Cancel()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = DateTime.Now.AddDays(1);
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = DateTime.Now;
        var enrollmentDeadline = DateTime.Now.AddDays(-1);
        var aggregate = new PublicGathering(name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        aggregate.Publish();
        var cancellationDate = this._fixture.Create<DateTime>();
        // Act
        aggregate.Cancel(cancellationDate);
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(PublicGatheringStatus.Cancelled);
        
        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<PublicGatheringStatusChanged>();
        
        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<PublicGatheringCancelledDomainEvent>();
    }
}