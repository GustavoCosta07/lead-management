
namespace MyApp.Domain.Events
{
    public class LeadDeclinedEvent : BaseDomainEvent
    {
        public Guid LeadId { get; }

        public LeadDeclinedEvent(Guid leadId)
        {
            LeadId = leadId;
        }
    }
}
