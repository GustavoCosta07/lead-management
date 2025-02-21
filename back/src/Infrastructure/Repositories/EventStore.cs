using MyApp.Application.Interfaces;
using MyApp.Domain.Events;
using MyApp.Infrastructure.Persistence;
using Newtonsoft.Json;
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
        var eventData = JsonConvert.SerializeObject(@event);

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

        return events.Select(e => JsonConvert.DeserializeObject<BaseDomainEvent>(e.Data)).ToList();
    }
}
