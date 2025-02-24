using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Query
{
    public record GetLeadsQuery : IRequest<List<LeadSummaryDto>>;
}