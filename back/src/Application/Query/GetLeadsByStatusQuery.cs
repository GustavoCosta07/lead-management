using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Query
{
    public record GetLeadsByStatusQuery(string Status) : IRequest<List<LeadDetailsDto>>;
}