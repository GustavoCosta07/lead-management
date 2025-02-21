using Moq;
using MyApp.API.Controllers;
using MyApp.Application.Commands;
using MyApp.Application.Query;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Tests.API.Controllers
{
    public class LeadControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILeadRepository> _mockLeadRepository;
        private readonly LeadController _controller;

        public LeadControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLeadRepository = new Mock<ILeadRepository>();
            _controller = new LeadController(_mockMediator.Object, _mockLeadRepository.Object);
        }

        [Fact]
        public async Task GetLeads_ReturnsOkResult_WithListOfLeads()
        {
            var leads = new List<Lead> 
            { 
                new Lead("John", "Doe", "john.doe@example.com", "1234567890", "Example Company", 1000000M, "Web", "New") 
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetLeadsQuery>(), default)).ReturnsAsync(leads);

            var result = await _controller.GetLeads();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnLeads = Assert.IsType<List<Lead>>(okResult.Value);
            Assert.Equal(leads.Count, returnLeads.Count);
        }

        [Fact]
        public async Task GetLeadsByStatus_ReturnsOkResult_WithListOfLeads()
        {
            var status = "New";
            var leads = new List<Lead> 
            { 
                new Lead("Jane", "Doe", "jane.doe@example.com", "0987654321", "Another Company", 2000000M, "Referral", status) 
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetLeadsByStatusQuery>(), default)).ReturnsAsync(leads);

            var result = await _controller.GetLeadsByStatus(status);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnLeads = Assert.IsType<List<Lead>>(okResult.Value);
            Assert.Equal(leads.Count, returnLeads.Count);
        }

        [Fact]
        public async Task AcceptLead_ReturnsNoContentResult()
        {
            var leadId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<AcceptLeadCommand>(), default)).ReturnsAsync(Unit.Value);

            var result = await _controller.AcceptLead(leadId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeclineLead_ReturnsNoContentResult()
        {
            var leadId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<DeclineLeadCommand>(), default)).ReturnsAsync(Unit.Value);

            var result = await _controller.DeclineLead(leadId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
