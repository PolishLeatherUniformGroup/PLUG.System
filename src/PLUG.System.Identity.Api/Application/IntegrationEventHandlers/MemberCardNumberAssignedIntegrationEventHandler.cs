using PLUG.System.EmailService.Abstractions;
using PLUG.System.EventBus.Abstraction;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Identity.Api.Application.IntegrationEventHandlers;

public sealed class MemberCardNumberAssignedIntegrationEventHandler : IIntegrationEventHandler<MemberCardNumberAssignedIntegrationEvent>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;

    public MemberCardNumberAssignedIntegrationEventHandler(UserManager<ApplicationUser> userManager, IEmailService emailService)
    {
        this._userManager = userManager;
        this._emailService = emailService;
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
        // send Email
        if (result.Succeeded)
        {
            await this._emailService.SendMessage("", @event.Email, "");
        }
    }
}