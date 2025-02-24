using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces;
using MyApp.Domain.Events;
using MyApp.Infrastructure.Persistence;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EventStore : IEventStore
{
    private readonly AppDbContext _dbContext;

    public EventStore(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AppendAsync<T>(T @event) where T : BaseDomainEvent
    {
        var eventType = @event.GetType().Name;
        var eventData = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        });

        var eventEntity = new Event
        {
            Id = Guid.NewGuid(),
            EventType = eventType,
            Data = eventData,
            OccurredOn = @event.OccurredOn,
            AggregateId = @event.AggregateId
        };

        await _dbContext.Events.AddAsync(eventEntity);
        await _dbContext.SaveChangesAsync();
    }

    public IEnumerable<BaseDomainEvent> GetEvents(Guid aggregateId)
    {
        var events = _dbContext.Events
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.OccurredOn)
            .ToList();

        return events.Select(e => JsonConvert.DeserializeObject<BaseDomainEvent>(e.Data, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        })).ToList();
    }

    public async Task SaveSnapshotAsync<T>(T snapshot) where T : BaseDomainEvent
    {
        var snapshotData = JsonConvert.SerializeObject(snapshot, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        });

        var snapshotEntity = new Event
        {
            Id = Guid.NewGuid(),
            EventType = "Snapshot",
            Data = snapshotData,
            OccurredOn = DateTime.UtcNow,
            AggregateId = snapshot.AggregateId
        };

        await _dbContext.Events.AddAsync(snapshotEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<T> GetLatestSnapshotAsync<T>(Guid aggregateId) where T : BaseDomainEvent
    {
        var snapshot = await _dbContext.Events
            .Where(e => e.AggregateId == aggregateId && e.EventType == "Snapshot")
            .OrderByDescending(e => e.OccurredOn)
            .FirstOrDefaultAsync();

        return snapshot != null ? JsonConvert.DeserializeObject<T>(snapshot.Data, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects
        }) : null;
    }
}