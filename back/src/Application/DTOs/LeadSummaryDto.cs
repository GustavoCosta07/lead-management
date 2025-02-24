using MyApp.Domain.Enums;

namespace MyApp.Application.DTOs
{
    public class LeadSummaryDto
    {
        public Guid Id { get; set; }
        public string ContactFirstName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Suburb { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public LeadStatus Status { get; set; }
    }
}