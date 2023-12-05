namespace ONPA.Common.Domain;

public interface IAggregateRoot
{
    Guid AggregateId { get; }
    long Version { get; }
    
    IEnumerable<IStateEvent> GetStateEvents();
    void ApplyStateChanges();
    void ClearChanges();

    IEnumerable<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();

}