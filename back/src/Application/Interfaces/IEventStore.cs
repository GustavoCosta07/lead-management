using MyApp.Domain.Events;

namespace MyApp.Application.Interfaces
{
    public interface IEventStore
    {
        Task AppendAsync<T>(T @event) where T : BaseDomainEvent;
        IEnumerable<BaseDomainEvent> GetEvents(Guid aggregateId);
    }
}
