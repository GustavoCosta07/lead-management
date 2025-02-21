public class Event
{
    public Guid Id { get; set; }
    public string EventType { get; set; }
    public string Data { get; set; }
    public DateTime OccurredOn { get; set; }
    public Guid AggregateId { get; set; }
}

