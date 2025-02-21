using MediatR;
using MyApp.Application.Query;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Enums;

public class GetLeadsByStatusHandler : IRequestHandler<GetLeadsByStatusQuery, List<Lead>> {
    private readonly ILeadRepository _leadRepository;

    public GetLeadsByStatusHandler(ILeadRepository leadRepository) {
        _leadRepository = leadRepository;
    }

    public async Task<List<Lead>> Handle(GetLeadsByStatusQuery request, CancellationToken cancellationToken) {
        if (!Enum.TryParse(request.Status, true, out LeadStatus leadStatus)) {
            throw new ArgumentException("Invalid status value.");
        }

        var leads = await _leadRepository.GetByStatusAsync(leadStatus);
        return leads.ToList();
    }
}
