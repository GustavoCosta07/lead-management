using MediatR;
using MyApp.Domain.Entities;

namespace MyApp.Application.Query
{
    public record GetLeadsQuery : IRequest<List<Lead>>;
}
