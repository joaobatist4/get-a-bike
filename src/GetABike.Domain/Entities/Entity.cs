using GetABike.Domain.Interfaces;

namespace GetABike.Domain.Entities;

public class Entity
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
}

public abstract class AggregateRoot : Entity
{
    private List<IDomainEvent> _domainEvents;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearEvents(IDomainEvent domainEvent)
        => _domainEvents.Clear();

}