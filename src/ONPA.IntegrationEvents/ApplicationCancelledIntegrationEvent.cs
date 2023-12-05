using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record ApplicationCancelledIntegrationEvent : IntegrationEvent
 {
     public string FirstName { get; init; }
     public string Email { get; init; }
     public string Reason { get; set; }
 
     public ApplicationCancelledIntegrationEvent(string firstName, string email, string reason)
     {
         this.FirstName = firstName;
         this.Email = email;
         this.Reason = reason;
     }
 }