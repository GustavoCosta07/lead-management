using MyApp.Domain.Events;

namespace MyApp.Domain.Common
{
    public abstract class AggregateRoot
    {
        private readonly List<BaseDomainEvent> _domainEvents = new List<BaseDomainEvent>();

        public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(BaseDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
