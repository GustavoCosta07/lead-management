using MediatR;

namespace MyApp.Application.Commands
{
    public class AcceptLeadCommand : IRequest
    {
        public Guid LeadId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
