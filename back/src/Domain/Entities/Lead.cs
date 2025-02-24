using MyApp.Domain.Events;
using MyApp.Domain.Common;
using MyApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class Lead : AggregateRoot
    {
        public Guid Id { get; internal set; }
        public string? ContactFirstName { get; internal set; }
        public string? ContactFullName { get; internal set; }
        public string? ContactPhoneNumber { get; internal set; }
        public string? ContactEmail { get; internal set; }
        public DateTime DateCreated { get; internal set; }
        public string? Suburb { get; internal set; }
        public string? Category { get; internal set; }
        public string? Description { get; internal set; }
        public decimal Price { get; internal set; }
        public LeadStatus Status { get; internal set; }

        private readonly List<BaseDomainEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public Lead() { }

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

        public void ApplySnapshot(LeadSnapshot snapshot)
        {
            this.Id = snapshot.AggregateId;
            this.ContactFirstName = snapshot.ContactFirstName;
            this.ContactFullName = snapshot.ContactFullName;
            this.ContactPhoneNumber = snapshot.ContactPhoneNumber;
            this.ContactEmail = snapshot.ContactEmail;
            this.DateCreated = snapshot.DateCreated;
            this.Suburb = snapshot.Suburb;
            this.Category = snapshot.Category;
            this.Description = snapshot.Description;
            this.Price = snapshot.Price;
            this.Status = snapshot.Status;
        }

        private void AddDomainEvent(BaseDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void Rehydrate(IEnumerable<BaseDomainEvent> events)
        {
            foreach (var @event in events)
            {
                switch (@event)
                {
                    case LeadCreatedEvent createdEvent:
                        this.Id = createdEvent.LeadId;
                        this.ContactFirstName = createdEvent.ContactFirstName;
                        this.ContactFullName = createdEvent.ContactFullName;
                        this.ContactPhoneNumber = createdEvent.ContactPhoneNumber;
                        this.ContactEmail = createdEvent.ContactEmail;
                        this.DateCreated = createdEvent.DateCreated;
                        this.Suburb = createdEvent.Suburb;
                        this.Category = createdEvent.Category;
                        this.Description = createdEvent.Description;
                        this.Price = createdEvent.Price;
                        this.Status = LeadStatus.Invited;
                        break;

                    case LeadAcceptedEvent acceptedEvent:
                        this.Status = LeadStatus.Accepted;
                        this.Price = acceptedEvent.FinalPrice;
                        break;

                    case LeadDeclinedEvent declinedEvent:
                        this.Status = LeadStatus.Declined;
                        break;
                }
            }
        }
    }
}
