using MediatR;

namespace MyApp.Application.Commands
{
    public class DeclineLeadCommand : IRequest
    {
        public Guid LeadId { get; set; }
    }
}
