using MyApp.Domain.Events;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Services
{
    public class LeadProjectionService
    {
        private readonly ILeadRepository _leadRepository;

        public LeadProjectionService(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public async Task ApplyEventAsync(BaseDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case LeadAcceptedEvent acceptedEvent:
                    var lead = await _leadRepository.GetByIdAsync(acceptedEvent.LeadId);
                    lead.Accept();
                    await _leadRepository.UpdateAsync(lead);
                    break;

                case LeadDeclinedEvent declinedEvent:
                    var declinedLead = await _leadRepository.GetByIdAsync(declinedEvent.LeadId);
                    declinedLead.Decline();
                    await _leadRepository.UpdateAsync(declinedLead);
                    break;
            }
        }
    }
}