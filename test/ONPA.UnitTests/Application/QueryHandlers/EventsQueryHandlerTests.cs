using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Api.Application.QueryHandlers;
using ONPA.Gatherings.Api.Maps;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;
using ONPA.Organizations.Api.Maps;

namespace ONPA.UnitTests.Application.QueryHandlers;

public class EventsQueryHandlerTests
{
    private readonly IFixture _fixture = new Fixture();
    private readonly IReadOnlyRepository<Event> _eventRepository;
    private readonly IReadOnlyRepository<EventEnrollment> _eventEnrollmentRepository;
    private readonly IMapper _mapper;
    
    public EventsQueryHandlerTests()
    {
        _eventRepository = Substitute.For<IReadOnlyRepository<Event>>();
        _eventEnrollmentRepository = Substitute.For<IReadOnlyRepository<EventEnrollment>>();
        
        this._mapper =  new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EventsMap>();
            cfg.ShouldUseConstructor = ci => ci.IsPublic;
        }).CreateMapper();
    }
    
    [Fact]
    public async Task GetEventByIdQueryHandler_ReturnsEvent()
    {
        // Arrange
        var @event = _fixture.Create<Event>();
        var query = this._fixture.Create<GetEventQuery>();
        _eventRepository.ReadSingleById(@query.EventId, Arg.Any<CancellationToken>()).Returns(@event);
        var handler = new GetEventQueryHandler(_eventRepository, _mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<EventResponse>();
    }
    
    [Fact]
    public async Task GetEventsByStatusQueryHandler_ReturnsEvents()
    {
        // Arrange
        var events = _fixture.CreateMany<Event>();
        var query = this._fixture.Create<GetEventsByStatusQuery>();
        _eventRepository.ManyByFilter(Arg.Any<Expression<Func<Event, bool>>>(),Arg.Any<int>(),Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(events);
        var handler = new GetEventsByStatusQueryHandler(_eventRepository, _mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CollectionResult<EventResponse>>();
    }
    
    [Fact]
    public async Task GetEventEnrollmentsQueryHandler_ReturnsEventEnrollments()
    {
        // Arrange
        var eventEnrollments = _fixture.CreateMany<EventEnrollment>();
        var query = this._fixture.Create<GetEventEnrollmentsQuery>();
        _eventEnrollmentRepository.ManyByFilter(Arg.Any<Expression<Func<EventEnrollment, bool>>>(),Arg.Any<int>(),Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(eventEnrollments);
        var handler = new GetEventEnrollmentsQueryHandler(_eventEnrollmentRepository, _mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CollectionResult<EnrollmentResponse>>();
    }
    
    [Fact]
    public async Task GetEventEnrollmentQueryHandler_ReturnsEventEnrollment()
    {
        // Arrange
        var eventEnrollment = _fixture.Create<EventEnrollment>();
        var query = this._fixture.Create<GetEventEnrollmentQuery>();
        _eventEnrollmentRepository.ReadSingleById(@query.EnrollmentId, Arg.Any<CancellationToken>()).Returns(eventEnrollment);
        var handler = new GetEventEnrollmentQueryHandler(_eventEnrollmentRepository, _mapper);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<EnrollmentResponse>();
    }
    
    [Fact]
    public async Task GetEventParticipantsQueryHandler_ReturnsEventParticipants()
    {
        // Arrange
        var enrollment = _fixture.Create<EventEnrollment>();
        var eventParticipants = _fixture.CreateMany<EventParticipant>();
        enrollment.Participants = eventParticipants.ToList();
        var query = this._fixture.Create<GetEventParticipantsQuery>();
        _eventEnrollmentRepository.ReadSingleById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(enrollment);
        var handler = new GetEventParticipantsQueryHandler(_eventEnrollmentRepository);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CollectionResult<ParticipantResponse>>();
    }
}