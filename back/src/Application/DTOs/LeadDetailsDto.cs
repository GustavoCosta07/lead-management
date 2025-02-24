namespace MyApp.Application.DTOs
{
    public class LeadDetailsDto : LeadSummaryDto
    {
        public string ContactFullName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
    }
}