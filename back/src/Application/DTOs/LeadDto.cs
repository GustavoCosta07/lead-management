using MyApp.Domain.Enums;

namespace MyApp.Application.DTOs
{
    public class LeadDto
    {
        public Guid Id { get; set; }
        public string? ContactFirstName { get; set; }
        public string? ContactFullName { get; set; }
        public string? ContactPhoneNumber { get; set; }
        public string? ContactEmail { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Suburb { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public LeadStatus Status { get; set; }
    }
}