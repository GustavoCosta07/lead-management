
namespace MyApp.Domain.Events
{
    public class LeadAcceptedEvent : BaseDomainEvent
    {
        public Guid LeadId { get; }
        public decimal FinalPrice { get; }

        public LeadAcceptedEvent(Guid leadId, decimal finalPrice)
        {
            LeadId = leadId;
            FinalPrice = finalPrice;
            AggregateId = leadId;
        }

    }
}
