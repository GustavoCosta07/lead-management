using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Handlers
{
    public class DeclineLeadHandler : IRequestHandler<DeclineLeadCommand>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IEventStore _eventStore;

        public DeclineLeadHandler(ILeadRepository leadRepository, IEventStore eventStore)
        {
            _leadRepository = leadRepository;
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(DeclineLeadCommand request, CancellationToken cancellationToken)
        {
            var lead = await _leadRepository.GetByIdAsync(request.LeadId);
            if (lead == null) throw new KeyNotFoundException("Lead not found");

            lead.Decline();
            await _leadRepository.UpdateAsync(lead);

            foreach (var domainEvent in lead.DomainEvents)
            {
                await _eventStore.AppendAsync(domainEvent);
            }

            lead.ClearDomainEvents();

            return Unit.Value;
        }
    }
}
