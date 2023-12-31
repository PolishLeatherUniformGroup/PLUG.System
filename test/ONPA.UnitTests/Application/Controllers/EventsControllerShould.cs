﻿using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ONPA.Common.Application;
using ONPA.Common.Queries;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Api.Application.Queries;
using ONPA.Gatherings.Api.Controllers;
using ONPA.Gatherings.Api.Maps;
using ONPA.Gatherings.Api.Services;
using ONPA.Gatherings.Contract.Requests;
using ONPA.Gatherings.Contract.Responses;

namespace ONPA.UnitTests.Application.Controllers;

public class EventsControllerShould
{
    private readonly EventsController _sut;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IIdentityService _identityService;
    private readonly IFixture _fixture;
    private readonly Guid _tenantId = Guid.NewGuid();
    
    public EventsControllerShould()
    {
        this._fixture = new Fixture();
        this._mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EventsMap>();
            cfg.ShouldUseConstructor = ci => ci.IsPublic;
        }).CreateMapper();
        this._mediator = Substitute.For<IMediator>();
        this._identityService = Substitute.For<IIdentityService>();
        this._identityService.GetUserOrganization().Returns(this._tenantId);
        this._sut = new EventsController(this._mediator,this._mapper, this._identityService);
    }
    
    [Fact]
    public async Task ReturnOkWhenEventIsCreated()
    {
        // Arrange
        var request = this._fixture.Create<CreateEventRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<CreateEventCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.CreateEvent(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventIsUpdated()
    {
        // Arrange
        var request = this._fixture.Create<UpdateEventDescriptionRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<ModifyEventDescriptionCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.UpdateEvent(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventPriceIsUpdated()
    {
        // Arrange
        var request = this._fixture.Create<UpdateEventPriceRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<ModifyEventPriceCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.UpdateEventPrice(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventScheduleIsUpdated()
    {
        // Arrange
        var request = this._fixture.Create<UpdateEventScheduleRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<ModifyEventScheduleCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.UpdateEventSchedule(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventCapacityIsUpdated()
    {
        // Arrange
        var request = this._fixture.Create<UpdateEventCapacityRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<ModifyEventCapacityCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.UpdateEventCapacity(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventIsCancelled()
    {
        // Arrange
        var request = this._fixture.Create<CancelEventRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<CancelEventCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.CancelEvent(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    
    [Fact]
    public async Task ReturnOkWhenEventIsDeleted()
    {
        // Arrange
        var request = this._fixture.Create<ArchiveEventRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<ArchiveEventCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.ArchiveEvent(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventIsPublished()
    {
        // Arrange
        var request = this._fixture.Create<PublishEventRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<PublishEventCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.PublishEvent(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventIsAccepted()
    {
        // Arrange
        var request = this._fixture.Create<AcceptEventRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<AcceptEventCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.AcceptEvent(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEnrolledToEvent()
    {
        // Arrange
        var request = this._fixture.Create<CreateEventEnrollmentRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<EnrollToEventCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.EnrollToEvent(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEnrollmentPaid()
    {
        // Arrange
        var request = this._fixture.Create<RegisterEnrollmentPaymentRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<RegisterEnrollmentPaymentCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.EnrollToEvent(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEnrollmentIsCancelled()
    {
        // Arrange
        var request = this._fixture.Create<CancelEnrollmentRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<CancelEnrollmentCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.CancelEnrollment(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEnrollmentIsRefunded()
    {
        // Arrange
        var request = this._fixture.Create<RefundEnrollmentPaymentRequest>();
        var response = this._fixture.Create<Guid>();
        this._mediator.Send(Arg.Any<RefundCancelledEnrollmentCommand>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.RefundEnrollment(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<Guid>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    
    [Fact]
    public async Task ReturnOkWhenEventEnrollmentsAreRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetEventEnrollmentsRequest>();
        var response = this._fixture.CreateMany<EnrollmentResponse>(5);
        this._mediator.Send(Arg.Any<GetEventEnrollmentsQuery>(), Arg.Any<CancellationToken>())
            .Returns(CollectionResult<EnrollmentResponse>.FromValue(response,5));
        
        // Act
        var result = await this._sut.GetEventEnrollments(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<EnrollmentResponse>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventParticipantsAreRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetEventParticipantsRequest>();
        var response = this._fixture.CreateMany<ParticipantResponse>(5);
        this._mediator.Send(Arg.Any<GetEventParticipantsQuery>(), Arg.Any<CancellationToken>())
            .Returns(CollectionResult<ParticipantResponse>.FromValue(response,5));
        
        // Act
        var result = await this._sut.GetEventParticipants(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<ParticipantResponse>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventsAreRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetEventsByStatusRequest>();
        var response = this._fixture.CreateMany<EventResponse>(5);
        this._mediator.Send(Arg.Any<GetEventsByStatusQuery>(), Arg.Any<CancellationToken>())
            .Returns(CollectionResult<EventResponse>.FromValue(response,5));
        
        // Act
        var result = await this._sut.GetEvents(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<PageableResult<EventResponse>>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async Task ReturnOkWhenEventIsRetrieved()
    {
        // Arrange
        var request = this._fixture.Create<GetEventRequest>();
        var response = this._fixture.Create<EventResponse>();
        this._mediator.Send(Arg.Any<GetEventQuery>(), Arg.Any<CancellationToken>())
            .Returns(response);
        
        // Act
        var result = await this._sut.GetEvent(request);
        
        // Assert
        result.Should().BeOfType<ActionResult<EventResponse?>>();
        result.Result.Should().BeOfType<OkObjectResult>();
    }
    
}