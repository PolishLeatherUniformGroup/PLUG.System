using AutoFixture;
using FluentAssertions;
using ONPA.Common.Exceptions;
using ONPA.Gatherings.Domain;
using ONPA.Gatherings.DomainEvents;
using ONPA.Gatherings.StateEvents;
using PLUG.System.SharedDomain;

namespace ONPA.UnitTests.Domain;

public class EventShould
{
    private readonly IFixture _fixture;
    private readonly Guid _tenantId = Guid.NewGuid();

    public EventShould()
    {
        this._fixture = new Fixture();
    }

    [Fact]
    public void CreateEvent()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.Status.Should().Be(EventStatus.Draft);
        aggregate.IsCostFree.Should().BeFalse();
        aggregate.IsAvailable.Should().BeFalse();
        aggregate.Registrations.Should().BeEmpty();
        aggregate.Version.Should().BeGreaterThan(0);

        aggregate.GetStateEvents().Should().HaveCount(1);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventDescriptionModified>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EventDescriptionChangedDomainEvent>();
    }

    [Fact]
    public void ChangeDescriptionAndNotify()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        var newName = this._fixture.Create<string>();
        var newDescription = this._fixture.Create<string>();
        var newRegulations = this._fixture.Create<string>();
        
        //Act
        aggregate.ModifyDescriptions(newName, newDescription, newRegulations);
        
        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Name.Should().Be(newName);
        aggregate.Description.Should().Be(newDescription);
        aggregate.Regulations.Should().Be(newRegulations);

        aggregate.GetStateEvents().Should().HaveCount(5);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventDescriptionModified>();

        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EventDescriptionChangedDomainEvent>();
    }
    
    [Fact]
    public void Throw_onChangeDescriptions_whenArchived()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newName = this._fixture.Create<string>();
        var newDescription = this._fixture.Create<string>();
        aggregate.Archive();
        // Act
        var action = () => aggregate.ModifyDescriptions(newName, newDescription, regulations);

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newCapacity = this._fixture.Create<int>();

        // Act
        aggregate.ModifyCapacity(newCapacity);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.PlannedCapacity.Should().Be(newCapacity);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCapacityChanged>();
    }

    [Fact]
    public void Throw_onModifyCapacity_whenArchived()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newCapacity = this._fixture.Create<int>();
        aggregate.Archive();
        // Act
        var action = () => aggregate.ModifyCapacity(newCapacity);

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
    }

    [Fact]
    public void Throw_onModifyCapacity_whenDecreasingCapacityOfPublished()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newCapacity = plannedCapacity - 1;
        aggregate.Accept();
        aggregate.Publish();

        // Act
        var action = () => aggregate.ModifyCapacity(newCapacity);

        // Assert
        action.Should().Throw<InvalidDomainOperationException>();
    }

    [Fact]
    public void ModifyCapacity_whenIncreasingCapacityOfPublished()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newCapacity = plannedCapacity + 1;
        aggregate.Accept();
        aggregate.Publish();

        // Act
        aggregate.ModifyCapacity(newCapacity);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.PlannedCapacity.Should().Be(newCapacity);

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCapacityChanged>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EventCapacityIncreasedDomainEvent>();
    }

    [Fact]
    public void ModifyCapacity_whenRemoveLimitOfPublished()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newCapacity = null as int?;
        aggregate.Accept();
        aggregate.Publish();

        // Act
        aggregate.ModifyCapacity(newCapacity);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.PlannedCapacity.Should().BeNull();

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCapacityChanged>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EventCapacityIncreasedDomainEvent>();
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newPrice = new Money(this._fixture.Create<decimal>());

        // Act
        aggregate.ModifyPrice(newPrice);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.PricePerPerson.Should().Be(newPrice);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventPriceChanged>();
    }

    [Fact]
    public void Thrown_onModifyPrice_whenArchived()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newPrice = new Money(this._fixture.Create<decimal>());
        aggregate.Archive();
        // Act
        var action = () => aggregate.ModifyPrice(newPrice);

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventScheduleChanged>();
    }

    [Fact]
    public void Throw_onModifySchedule_whenArchived()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        var newScheduledStart = this._fixture.Create<DateTime>();
        var newPublishDate = this._fixture.Create<DateTime>();
        var newEnrollmentDeadline = this._fixture.Create<DateTime>();
        aggregate.Archive();

        // Act
        var action = () => aggregate.ModifySchedule(newScheduledStart, newPublishDate, newEnrollmentDeadline);

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);

        // Act
        aggregate.Archive();

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(EventStatus.Archived);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
    }

    [Fact]
    public void Throw_onArchive_whenAlreadyArchived()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Archive();
        // Act
        var action = () => aggregate.Archive();

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
    }

    [Fact]
    public void Throw_onArchive_whenCallBeforeFinish()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = DateTime.UtcNow.AddDays(1);
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = this._fixture.Create<DateTime>();
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        aggregate.Publish();
        // Act
        var action = () => aggregate.Archive();

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);

        // Act
        aggregate.Accept();

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(EventStatus.ReadyToPublish);

        aggregate.GetStateEvents().Should().HaveCount(2);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
    }

    [Fact]
    public void Throw_onAccept_whenNotDraft()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        // Act
        var action = () => aggregate.Accept();

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();

        // Act
        aggregate.Publish();

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(EventStatus.Published);

        aggregate.GetStateEvents().Should().HaveCount(3);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
    }

    [Fact]
    public void Throw_onPublish_whenDraft()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);

        // Act
        var action = () => aggregate.Publish();

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
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
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.Status.Should().Be(EventStatus.Published);
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
        aggregate.AvailablePlaces.Should().Be(plannedCapacity - bookedPlaces);

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEvent>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEventDomainEvent>();
    }
    
    [Fact]
    public void EnrollToNotLimitedEvent()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = null as int?;
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.Status.Should().Be(EventStatus.Published);
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
        aggregate.AvailablePlaces.Should().Be(plannedCapacity - bookedPlaces);

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEvent>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEventDomainEvent>();
    }

    [Fact]
    public void Throw_onEnroll_whenNotAvailable_dueTtoNotPublished()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();

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
        var action = () => aggregate.Enroll(registrationDate, bookedPlaces, firstName, lastName, email, participants);

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
    }
    
    [Fact]
    public void Throw_onEnroll_whenNotAvailable_dueToOverDeadline()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(-1);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();

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
        var action = () => aggregate.Enroll(registrationDate, bookedPlaces, firstName, lastName, email, participants);

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
    }
    
    [Fact]
    public void Throw_onEnroll_whenNotAvailable_dueToCapacity()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = 2;
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        aggregate.Publish();
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var participants = new List<Participant>
        {
            new(firstName, lastName, email),
            new(firstName, lastName, email),
            new(firstName, lastName, email)
        };
        var registrationDate = this._fixture.Create<DateTime>();
        var bookedPlaces = 3;

        // Act
        var action = () => aggregate.Enroll(registrationDate, bookedPlaces, firstName, lastName, email, participants);

        // Assert
        action.Should().Throw<InvalidDomainOperationException>();
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
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.Status.Should().Be(EventStatus.Published);
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
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEvent>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegistered>();

        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEventDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegisteredDomainEvent>();
    }
    
     [Fact]
    public void NotRegisterPaymentForEnrollment_whenEnrollmentIsCostFree()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(0);
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.Status.Should().Be(EventStatus.Published);
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
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEvent>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEventDomainEvent>();
     
    }

    [Fact]
    public void Throw_onRegisterPaymentForEnrollment_whenEnrollmentDoesNotExisits()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        var action = ()=>aggregate.RegisterEnrollmentPayment(Guid.NewGuid(), paidDate, paidAmount);

        // Assert
        action.Should().Throw<EntityNotFoundException>();
    }
    
    [Fact]
    public void Throw_onRegisterPaymentForEnrollment_whenEventCancelled()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.Cancel(this._fixture.Create<DateTime>(), this._fixture.Create<string>());
        // Act
        var action = ()=>aggregate.RegisterEnrollmentPayment(enrollment.Id, paidDate, paidAmount);

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
    }
    
    [Fact]
    public void Throw_onRegisterPaymentForEnrollment_whenEnrollmentCancelled()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.CancelEnrollment(enrollment.Id,this._fixture.Create<DateTime>(), pricePerPerson);
        // Act
        var action = ()=>aggregate.RegisterEnrollmentPayment(enrollment.Id, paidDate, paidAmount);

        // Assert
        action.Should().Throw<InvalidDomainOperationException>();
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
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.Status.Should().Be(EventStatus.Published);
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
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEvent>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegistered>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentCancelled>();

        aggregate.GetDomainEvents().Should().HaveCount(3);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEventDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegisteredDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentCancelledDomainEvent>();
    }
    
    [Fact]
    public void Throw_onCancelEnrollment_whenEnrollmentDoesNotExists()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        var action = ()=>aggregate.CancelEnrollment(Guid.NewGuid(), cancellationDate, refundableAmount);

        // Assert
        action.Should().Throw<EntityNotFoundException>();
    }

    [Fact]
    public void RefundCanceledEnrollment()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = null as int?;
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.Status.Should().Be(EventStatus.Published);
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
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEvent>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegistered>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentCancelled>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentRefunded>();

        aggregate.GetDomainEvents().Should().HaveCount(4);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEventDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentPaymentRegisteredDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentCancelledDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentRefundedDomainEvent>();
    }
    
    [Fact]
    public void NotRefundCanceledEnrollment_whenEnrollmentIsCostFree()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = null as int?;
        var pricePerPerson = new Money(0);
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.Status.Should().Be(EventStatus.Published);
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
        aggregate.Registrations.First().CancellationDate.Should().Be(cancellationDate);
        aggregate.Registrations.First().RefundableAmount.Should().BeEquivalentTo(pricePerPerson);
        aggregate.Registrations.First().RefundedAmount.Should().BeNull();
        aggregate.Registrations.First().RefundDate.Should().BeNull();
        aggregate.Registrations.First().IsRefunded.Should().BeFalse();

        aggregate.GetStateEvents().Should().HaveCount(5);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEvent>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentCancelled>();

        aggregate.GetDomainEvents().Should().HaveCount(2);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEventDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentCancelledDomainEvent>();
      
    }

    
    [Fact]
    public void Thrown_onRefundCanceledEnrollment_whenEnrollmentNotExists()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = null as int?;
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        var action =()=>aggregate.RefundEnrollment(Guid.NewGuid(), refundDate, refundAmount);

        // Assert
        action.Should().Throw<EntityNotFoundException>();
    }
    
    [Fact]
    public void Thrown_onRefundCanceledEnrollment_whenEnrollmentRefunded()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = this._fixture.Create<DateTime>();
        var plannedCapacity = null as int?;
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = this._fixture.Create<DateTime>();
        var enrollmentDeadline = DateTime.UtcNow.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
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
        aggregate.RefundEnrollment(enrollment.Id, refundDate, refundAmount);
        // Act
        var action =()=>aggregate.RefundEnrollment(enrollment.Id, refundDate, refundAmount);

        // Assert
        action.Should().Throw<InvalidDomainOperationException>();
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        aggregate.Publish();
        var cancellationDate = this._fixture.Create<DateTime>();
        var cancellationReason = this._fixture.Create<string>();
        // Act
        aggregate.Cancel(cancellationDate,cancellationReason);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(EventStatus.Cancelled);

        aggregate.GetStateEvents().Should().HaveCount(4);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();

        aggregate.GetDomainEvents().Should().HaveCount(1);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EventCancelledDomainEvent>();
    }
    
    [Fact]
    public void Cancel_andNotifyEnrolled()
    {
        // Arrange
        var name = this._fixture.Create<string>();
        var description = this._fixture.Create<string>();
        var regulations = this._fixture.Create<string>();
        var scheduledStart = DateTime.Now.AddDays(1);
        var plannedCapacity = this._fixture.Create<int>();
        var pricePerPerson = new Money(this._fixture.Create<decimal>());
        var publishDate = DateTime.Now;
        var enrollmentDeadline = DateTime.Now.AddDays(7);
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        aggregate.Publish();
        var cancellationDate = this._fixture.Create<DateTime>();
        var cancellationReason = this._fixture.Create<string>();
        
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
        
        // Act
        aggregate.Cancel(cancellationDate,cancellationReason);

        // Assert
        aggregate.Should().NotBeNull();
        aggregate.AggregateId.Should().NotBeEmpty();
        aggregate.Status.Should().Be(EventStatus.Cancelled);

        aggregate.GetStateEvents().Should().HaveCount(6);
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventCreated>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EventStatusChanged>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEvent>();
        aggregate.GetStateEvents().Should().ContainItemsAssignableTo<EnrollmentCancelled>();

        aggregate.GetDomainEvents().Should().HaveCount(3);
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EventCancelledDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentAddedToEventDomainEvent>();
        aggregate.GetDomainEvents().Should().ContainItemsAssignableTo<EnrollmentCancelledDomainEvent>();
    }
    
    [Fact]
    public void Thrown_onCancel_whenNotPublished()
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
        var aggregate = new Event(this._tenantId, name, description, regulations, scheduledStart, plannedCapacity,
            pricePerPerson, publishDate, enrollmentDeadline);
        aggregate.Accept();
        
        var cancellationDate = this._fixture.Create<DateTime>();
        var cancellationReason = this._fixture.Create<string>();
        // Act
        var action = ()=>aggregate.Cancel(cancellationDate,cancellationReason);

        // Assert
        action.Should().Throw<AggregateInvalidStateException>();
    }
}