using MediatR;
using MyApp.Application.Query;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Handlers
{
    public class GetLeadsHandler : IRequestHandler<GetLeadsQuery, List<Lead>>
    {
        private readonly ILeadRepository _leadRepository;

        public GetLeadsHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task<List<Lead>> Handle(GetLeadsQuery request, CancellationToken cancellationToken)
        {
            return await _leadRepository.GetAllAsync();
        }
    }
}
