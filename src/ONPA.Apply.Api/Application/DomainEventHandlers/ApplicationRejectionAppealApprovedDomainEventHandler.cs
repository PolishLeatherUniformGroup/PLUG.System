using ONPA.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;

namespace ONPA.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationRejectionAppealApprovedDomainEventHandler : DomainEventHandlerBase<ApplicationRejectionAppealApprovedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationRejectionAppealApprovedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationRejectionAppealApprovedDomainEvent notification, CancellationToken cancellationToken)
    {
        var approveIntegrationEvent = new ApplicationRejectionAppealApprovedIntegrationEvent(notification.FirstName, notification.Email,
            notification.ApproveDate);
        var memberJoinedIntegrationEvent = new MemberJoinedIntegrationEvent(notification.FirstName,notification.LastName,notification.Email,
            notification.Phone, notification.Address,notification.ApproveDate, notification.PaidFee. Amount,notification.PaidFee.Currency);
        
        await this._integrationEventService.AddAndSaveEventAsync(approveIntegrationEvent);
        await this._integrationEventService.AddAndSaveEventAsync(memberJoinedIntegrationEvent);
    }
    
    
}