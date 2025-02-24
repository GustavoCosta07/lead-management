namespace MyApp.Domain.Events
{
    public class LeadCreatedEvent : BaseDomainEvent
    {
        public Guid LeadId { get; }
        public string ContactFirstName { get; }
        public string ContactFullName { get; }
        public string ContactPhoneNumber { get; }
        public string ContactEmail { get; }
        public DateTime DateCreated { get; }
        public string Suburb { get; }
        public string Category { get; }
        public string Description { get; }
        public decimal Price { get; }

        public LeadCreatedEvent(
            Guid leadId,
            string contactFirstName,
            string contactFullName,
            string contactPhoneNumber,
            string contactEmail,
            DateTime dateCreated,
            string suburb,
            string category,
            string description,
            decimal price)
        {
            LeadId = leadId;
            ContactFirstName = contactFirstName;
            ContactFullName = contactFullName;
            ContactPhoneNumber = contactPhoneNumber;
            ContactEmail = contactEmail;
            DateCreated = dateCreated;
            Suburb = suburb;
            Category = category;
            Description = description;
            Price = price;
            AggregateId = leadId;
        }
    }
}