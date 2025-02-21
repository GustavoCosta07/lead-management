using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Handlers
{
    public class AcceptLeadHandler : IRequestHandler<AcceptLeadCommand>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IEmailService _emailService;
        private readonly IEventStore _eventStore;

        public AcceptLeadHandler(ILeadRepository leadRepository, IEmailService emailService, IEventStore eventStore)
        {
            _leadRepository = leadRepository;
            _emailService = emailService;
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(AcceptLeadCommand request, CancellationToken cancellationToken)
        {
            var lead = await _leadRepository.GetByIdAsync(request.LeadId);
            if (lead == null) throw new KeyNotFoundException("Lead not found");

            lead.Accept();

            await _leadRepository.UpdateAsync(lead);

            foreach (var domainEvent in lead.DomainEvents)
            {
                await _eventStore.AppendAsync(domainEvent);
            }

            lead.ClearDomainEvents();

            await _emailService.SendEmailAsync("vendas@test.com", "Lead Aceito", $"O lead {lead.Id} foi aceito com preço ajustado: {lead.Price}");

            return Unit.Value;
        }
    }


}
