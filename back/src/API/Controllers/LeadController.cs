using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Application.Query;
using MyApp.Domain.Interfaces;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/leads")]
    public class LeadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILeadRepository _leadRepository;

        public LeadController(IMediator mediator, ILeadRepository leadRepository)
        {
            _mediator = mediator;
            _leadRepository = leadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeads()
        {
            var leads = await _mediator.Send(new GetLeadsQuery());
            return Ok(leads);
        }

      [HttpGet("{status}/status")]
        public async Task<IActionResult> GetLeadsByStatus(string status) {
            try {
                var leads = await _mediator.Send(new GetLeadsByStatusQuery(status));
                return Ok(leads);
            } catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("{id}/accept")]
        public async Task<IActionResult> AcceptLead(Guid id)
        {
            await _mediator.Send(new AcceptLeadCommand { LeadId = id });
            return NoContent();
        }

        [HttpPost("{id}/decline")]
        public async Task<IActionResult> DeclineLead(Guid id)
        {
            await _mediator.Send(new DeclineLeadCommand { LeadId = id });
            return NoContent();
        }
    }
}
