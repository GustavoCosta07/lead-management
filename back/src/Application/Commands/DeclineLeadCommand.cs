using MediatR;

namespace MyApp.Application.Commands
{
    public record DeclineLeadCommand(Guid LeadId) : IRequest;
}