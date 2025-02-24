using AutoMapper;
using MediatR;
using MyApp.Application.DTOs;
using MyApp.Application.Query;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Enums;
using MyApp.Domain.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Handlers
{
    public class GetLeadsByStatusHandler : IRequestHandler<GetLeadsByStatusQuery, List<LeadDetailsDto>>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;
        private readonly IEventStore _eventStore;

        public GetLeadsByStatusHandler(ILeadRepository leadRepository, IMapper mapper, IEventStore eventStore)
        {
            _leadRepository = leadRepository;
            _mapper = mapper;
            _eventStore = eventStore;
        }

        public async Task<List<LeadDetailsDto>> Handle(GetLeadsByStatusQuery request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse(request.Status, true, out LeadStatus leadStatus))
            {
                throw new ArgumentException("Invalid status value.");
            }

            var leads = new List<Lead>();

            var leadEntities = await _leadRepository.GetByStatusAsync(leadStatus);

            foreach (var leadEntity in leadEntities)
            {
                var snapshot = await _eventStore.GetLatestSnapshotAsync<LeadSnapshot>(leadEntity.Id);

                if (snapshot != null)
                {
                    var lead = new Lead();
                    lead.ApplySnapshot(snapshot);
                    leads.Add(lead);
                }
                else
                {
                    var events = _eventStore.GetEvents(leadEntity.Id);
                    leadEntity.Rehydrate(events);
                    leads.Add(leadEntity);
                }
            }

            return _mapper.Map<List<LeadDetailsDto>>(leads);
        }
    }
}