using MediatR;
using MyApp.Domain.Entities;

namespace MyApp.Application.Query {
    public record GetLeadsByStatusQuery(string Status) : IRequest<List<Lead>>;
}
