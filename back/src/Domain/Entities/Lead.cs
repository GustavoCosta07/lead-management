using MyApp.Domain.Events;
using MyApp.Domain.Common;
using MyApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class Lead : AggregateRoot
    {
        public Guid Id { get; private set; }
        public string? ContactFirstName { get; private set; }
        public string? ContactFullName { get; private set; }
        public string? ContactPhoneNumber { get; private set; }
        public string? ContactEmail { get; private set; }
        public DateTime DateCreated { get; private set; }
        public string? Suburb { get; private set; }
        public string? Category { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public LeadStatus Status { get; private set; }

        private readonly List<BaseDomainEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private Lead() { }

        public Lead(string contactFirstName, string contactFullName, string suburb, string category, string description, decimal price, string email, string contactPhoneNumber)
        {
            Id = Guid.NewGuid();
            ContactFirstName = contactFirstName;
            ContactFullName = contactFullName;
            ContactPhoneNumber = contactPhoneNumber;
            ContactEmail = email;
            DateCreated = DateTime.UtcNow;
            Suburb = suburb;
            Category = category;
            Description = description;
            Price = price;
            Status = LeadStatus.Invited;
        }

        public void Accept()
        {
            if (Status != LeadStatus.Invited)
                throw new InvalidOperationException("O lead precisa estar no status 'Invited' para ser aceito.");

            if (Price > 500)
                Price -= Price * 0.10m;

            Status = LeadStatus.Accepted;

            var leadAcceptedEvent = new LeadAcceptedEvent(Id, Price);
            AddDomainEvent(leadAcceptedEvent);
        }

        public void Decline()
        {
            if (Status != LeadStatus.Invited)
                throw new InvalidOperationException("O lead precisa estar no status 'Invited' para ser recusado.");

            Status = LeadStatus.Declined;

            AddDomainEvent(new LeadDeclinedEvent(Id));
        }

        private void AddDomainEvent(BaseDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
