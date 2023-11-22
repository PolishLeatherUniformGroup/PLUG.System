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
using Arg = NSubstitute.Arg;

namespace PLUG.System.Apply.UnitTests.Application.CommandHandlers;

public class CreateApplicationFormCommandHandlerShould
{
    private readonly CreateApplicationFormCommandHandler _sut;

    private readonly IAggregateRepository<ApplicationForm> _aggregateRepository;
    
    private readonly IFixture _fixture;

    public CreateApplicationFormCommandHandlerShould()
    {
        this._aggregateRepository = Substitute.For<IAggregateRepository<ApplicationForm>>();
        this._fixture = new Fixture();
        this._sut = new CreateApplicationFormCommandHandler(this._aggregateRepository);
    }

    [Fact]
    public async Task HandleCommand_withSuccess()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(2).ToList();
        var command = new CreateApplicationFormCommand(firstName, lastName, email, phone, recommendations, address);
        
        // Act
        var result = await this._sut.Handle(command, new CancellationToken());
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        _ = this._aggregateRepository.ReceivedWithAnyArgs(1)
            .CreateAsync(Arg.Any<ApplicationForm>(), default);
    }
    
    [Fact]
    public async Task Throw_onNotDomainException()
    {
        // Arrange
        var firstName = this._fixture.Create<string>();
        var lastName = this._fixture.Create<string>();
        var email = this._fixture.Create<string>();
        var phone = this._fixture.Create<string>();
        var address = this._fixture.Create<string>();
        var recommendations = this._fixture.CreateMany<string>(2).ToList();
        var command = new CreateApplicationFormCommand(firstName, lastName, email, phone, recommendations, address);

        this._aggregateRepository
            .CreateAsync(Arg.Any<ApplicationForm>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new DataException());
        // Act
        await Assert.ThrowsAsync<DataException>(()=> this._sut.Handle(command, new CancellationToken()));
        
        // Assert
        _ = this._aggregateRepository.ReceivedWithAnyArgs(1)
            .CreateAsync(Arg.Any<ApplicationForm>(), default);
    }
    
}