using ONPA.EventBus.Abstraction;
using ONPA.IntegrationEvents;

namespace ONPA.Identity.Api.Application.IntegrationEventHandlers;

public sealed class MemberCardNumberAssignedIntegrationEventHandler : IIntegrationEventHandler<MemberCardNumberAssignedIntegrationEvent>
{
    private readonly UserManager<ApplicationUser> _userManager;
   

    public MemberCardNumberAssignedIntegrationEventHandler(UserManager<ApplicationUser> userManager)
    {
        this._userManager = userManager;
    }

    public async Task Handle(MemberCardNumberAssignedIntegrationEvent @event)
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
      
    }
}