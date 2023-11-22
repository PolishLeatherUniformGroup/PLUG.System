using System.Data;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PLUG.System.Apply.Api.Application.CommandHandlers;
using PLUG.System.Apply.Api.Application.Commands;
using PLUG.System.Apply.Domain;
using PLUG.System.Apply.StateEvents;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.UnitTests.Application.CommandHandlers;

public class ValidateApplicationCommandHandlerShould
{
    private readonly ValidateApplicationCommandHandler _sut;

    private readonly IAggregateRepository<ApplicationForm> _aggregateRepository;
    
    private readonly IFixture _fixture = new Fixture();

    public ValidateApplicationCommandHandlerShould()
    {
        this._aggregateRepository = Substitute.For<IAggregateRepository<ApplicationForm>>();
        this._sut = new ValidateApplicationCommandHandler(this._aggregateRepository);
        var stateEvent = this._fixture.Create<ApplicationFormCreated>();
        var aggregate = new ApplicationForm(Guid.NewGuid(), new[] { stateEvent });
        
        this._aggregateRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(aggregate);
    }

    [Fact]
    public async Task HandleCommand_withSuccess()
    {
        // Arrange
        var command = new ValidateApplicationCommand(Guid.NewGuid(), new List<(string, Guid?)>() ,new Money(120));

        // Act
        var result = await this._sut.Handle(command, new CancellationToken());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        _ = this._aggregateRepository.ReceivedWithAnyArgs(1)
            .GetByIdAsync(Arg.Any<Guid>(), default);
        _ = this._aggregateRepository.ReceivedWithAnyArgs(1)
            .UpdateAsync(Arg.Any<ApplicationForm>(), default);
    }
    
    [Fact]
    public async Task HandleCommand_withFailure()
    {
        // Arrange
        var command = new ValidateApplicationCommand(Guid.NewGuid(), new List<(string, Guid?)>() ,new Money(120));
     this._aggregateRepository.UpdateAsync(Arg.Any<ApplicationForm>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new DomainException());
        // Act
        var result = await this._sut.Handle(command, new CancellationToken());
        
        // Assert
        result.IsFailure.Should().BeTrue();
        _ = this._aggregateRepository.ReceivedWithAnyArgs(1)
            .GetByIdAsync(Arg.Any<Guid>(), default);
        _ = this._aggregateRepository.ReceivedWithAnyArgs(1)
            .UpdateAsync(Arg.Any<ApplicationForm>(), default);
    }
    
    [Fact]
    public async Task Throw_onNotDomainException()
    {
        // Arrange
        var command = new ValidateApplicationCommand(Guid.NewGuid(), new List<(string, Guid?)>() ,new Money(120));

        this._aggregateRepository
            .UpdateAsync(Arg.Any<ApplicationForm>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new DataException());
        // Act
        await Assert.ThrowsAsync<DataException>(()=> this._sut.Handle(command, new CancellationToken()));
        
        // Assert
        _ = this._aggregateRepository.ReceivedWithAnyArgs(1)
            .GetByIdAsync(Arg.Any<Guid>(), default);
        _ = this._aggregateRepository.ReceivedWithAnyArgs(1)
            .UpdateAsync(Arg.Any<ApplicationForm>(), default);
    }
    
}