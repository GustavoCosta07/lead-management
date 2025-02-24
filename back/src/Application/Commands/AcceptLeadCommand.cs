using MediatR;

namespace MyApp.Application.Commands
{
    public record AcceptLeadCommand(Guid LeadId, string? FullName, string? PhoneNumber, string? Email) : IRequest;
}
