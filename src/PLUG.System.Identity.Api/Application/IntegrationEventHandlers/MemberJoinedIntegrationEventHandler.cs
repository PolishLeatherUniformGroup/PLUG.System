using PLUG.System.EventBus.Abstraction;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Identity.Api.Application.IntegrationEventHandlers;

public sealed class MemberJoinedIntegrationEventHandler : IIntegrationEventHandler<MemberJoinedIntegrationEvent>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public MemberJoinedIntegrationEventHandler(UserManager<ApplicationUser> userManager)
    {
        this._userManager = userManager;
    }

    public async Task Handle(MemberJoinedIntegrationEvent @event)
    {
        var user = new ApplicationUser()
        {
            CardNumber = @event.CardNumber,
            Name = @event.FirstName,
            LastName = @event.LastName,
            Email = @event.Email,
            PhoneNumber = @event.Phone,
            UserName = @event.CardNumber,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            Id = @event.CardNumber,
        };
        var result = await this._userManager.CreateAsync(user);
        // send Email
    }
}