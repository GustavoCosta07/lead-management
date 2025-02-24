using Moq;
using MyApp.Application.Query;
using MyApp.Application.Handlers;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using AutoMapper;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Enums;

namespace MyApp.Tests.Application.Handlers
{
    public class GetLeadsHandlerTests
    {
        private readonly Mock<ILeadRepository> _mockLeadRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IEventStore> _mockEventStore;
        private readonly GetLeadsHandler _handler;

        public GetLeadsHandlerTests()
        {
            _mockLeadRepository = new Mock<ILeadRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockEventStore = new Mock<IEventStore>();
            _handler = new GetLeadsHandler(_mockLeadRepository.Object, _mockMapper.Object, _mockEventStore.Object);
        }

        [Fact]
        public async Task Handle_ReturnsListOfLeads()
        {
            var leads = new List<Lead>
        {
            new Lead("John", "Doe", "Suburb", "Category", "Description", 100, "john.doe@example.com", "1234567890"),
            new Lead("Jane", "Doe", "Suburb", "Category", "Description", 200, "jane.doe@example.com", "0987654321")
        };
            var leadDtos = new List<LeadSummaryDto>
        {
            new LeadSummaryDto { Id = Guid.NewGuid(), ContactFirstName = "John", DateCreated = DateTime.UtcNow, Suburb = "Suburb", Category = "Category", Description = "Description", Price = 100, Status = LeadStatus.Invited },
            new LeadSummaryDto { Id = Guid.NewGuid(), ContactFirstName = "Jane", DateCreated = DateTime.UtcNow, Suburb = "Suburb", Category = "Category", Description = "Description", Price = 200, Status = LeadStatus.Invited }
        };
            _mockLeadRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(leads);
            _mockMapper.Setup(m => m.Map<List<LeadSummaryDto>>(leads)).Returns(leadDtos);

            var result = await _handler.Handle(new GetLeadsQuery(), CancellationToken.None);

            Assert.Equal(leadDtos, result);
        }
    }
}
