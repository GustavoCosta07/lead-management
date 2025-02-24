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

        public LeadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeads()
        {
            var leads = await _mediator.Send(new GetLeadsQuery()).ConfigureAwait(false);
            return Ok(leads);
        }

        [HttpGet("{status}/status")]
        public async Task<IActionResult> GetLeadsByStatus(string status)
        {
            var leads = await _mediator.Send(new GetLeadsByStatusQuery(status)).ConfigureAwait(false);
            return Ok(leads);
        }

        [HttpPost("{id}/accept")]
        public async Task<IActionResult> AcceptLead(Guid id)
        {
            await _mediator.Send(new AcceptLeadCommand(id, null, null, null)).ConfigureAwait(false);
            return NoContent();
        }

        [HttpPost("{id}/decline")]
        public async Task<IActionResult> DeclineLead(Guid id)
        {
            await _mediator.Send(new DeclineLeadCommand(id)).ConfigureAwait(false);
            return NoContent();
        }
    }
}
