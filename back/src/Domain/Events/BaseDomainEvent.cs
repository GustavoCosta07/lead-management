
namespace MyApp.Domain.Events
{
    public abstract class BaseDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public Guid AggregateId { get; set; }
    }

}
