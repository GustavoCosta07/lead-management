using Moq;
using MyApp.API.Controllers;
using MyApp.Application.Commands;
using MyApp.Application.Query;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Domain.Enums;

namespace MyApp.Tests.API.Controllers
{
    public class LeadControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly LeadController _controller;

        public LeadControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new LeadController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetLeads_ReturnsOkResult_WithListOfLeads()
        {
            var leads = new List<LeadSummaryDto>
        {
            new LeadSummaryDto { Id = Guid.NewGuid(), ContactFirstName = "John", DateCreated = DateTime.UtcNow, Suburb = "Suburb", Category = "Category", Description = "Description", Price = 100, Status = LeadStatus.Invited }
        };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetLeadsQuery>(), default)).ReturnsAsync(leads);

            var result = await _controller.GetLeads();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnLeads = Assert.IsType<List<LeadSummaryDto>>(okResult.Value);
            Assert.Equal(leads.Count, returnLeads.Count);
        }

        [Fact]
        public async Task GetLeadsByStatus_ReturnsOkResult_WithListOfLeads()
        {
            var status = "Invited";
            var leads = new List<LeadDetailsDto>
        {
            new LeadDetailsDto { Id = Guid.NewGuid(), ContactFirstName = "Jane", DateCreated = DateTime.UtcNow, Suburb = "Suburb", Category = "Category", Description = "Description", Price = 200, Status = LeadStatus.Invited, ContactFullName = "Jane Doe", ContactPhoneNumber = "1234567890", ContactEmail = "jane.doe@example.com" }
        };
            _mockMediator.Setup(m => m.Send(It.IsAny<GetLeadsByStatusQuery>(), default)).ReturnsAsync(leads);

            var result = await _controller.GetLeadsByStatus(status);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnLeads = Assert.IsType<List<LeadDetailsDto>>(okResult.Value);
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
