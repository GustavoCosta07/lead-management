using MyApp.Domain.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces
{
    public interface IEventStore
    {
        Task AppendAsync<T>(T @event) where T : BaseDomainEvent;
        IEnumerable<BaseDomainEvent> GetEvents(Guid aggregateId);
        Task SaveSnapshotAsync<T>(T snapshot) where T : BaseDomainEvent;
        Task<T> GetLatestSnapshotAsync<T>(Guid aggregateId) where T : BaseDomainEvent;
    }
}