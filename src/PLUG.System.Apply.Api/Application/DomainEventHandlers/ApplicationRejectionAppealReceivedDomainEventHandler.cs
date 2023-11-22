using PLUG.System.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationRejectionAppealReceivedDomainEventHandler : DomainEventHandlerBase<ApplicationRejectionAppealReceivedDomainEvent>
 {
     private readonly IIntegrationEventService _integrationEventService;
 
     public ApplicationRejectionAppealReceivedDomainEventHandler(IIntegrationEventService integrationEventService)
     {
         this._integrationEventService = integrationEventService;
     }
 
     public override async Task Handle(ApplicationRejectionAppealReceivedDomainEvent notification, CancellationToken cancellationToken)
     {
         var integrationEvent = new ApplicationRejectionAppealReceivedIntegrationEvent(notification.FirstName, notification.Email,
             notification.AppealDate);
         
         await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
     }
 }