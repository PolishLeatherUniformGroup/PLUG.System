using ONPA.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;

namespace ONPA.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationRejectionAppealReceivedDomainEventHandler : DomainEventHandlerBase<ApplicationRejectionAppealReceivedDomainEvent>
 {
     private readonly IIntegrationEventService _integrationEventService;
 
     public ApplicationRejectionAppealReceivedDomainEventHandler(IIntegrationEventService integrationEventService)
     {
         this._integrationEventService = integrationEventService;
     }
 
     public override async Task Handle(ApplicationRejectionAppealReceivedDomainEvent notification, CancellationToken cancellationToken)
     {
         var integrationEvent = new ApplicationRejectionAppealReceivedIntegrationEvent(notification.TenantId,notification.FirstName, notification.Email,
             notification.AppealDate);
         
         await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
     }
 }