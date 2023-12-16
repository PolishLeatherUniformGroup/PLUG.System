using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Api.Controllers;
using ONPA.Gatherings.Api.Maps;
using ONPA.Gatherings.Api.Services;
using ONPA.Gatherings.Contract.Requests;
using ONPA.Gatherings.StateEvents;

namespace PLUG.System.Apply.UnitTests.Application;

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
    
}