using Moq;
using MyApp.Application.Query;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Enums;
using AutoMapper;
using MyApp.Application.DTOs;
using MyApp.Application.Handlers;
using MyApp.Application.Interfaces;

namespace MyApp.Tests.Application.Handlers
{
    public class GetLeadsByStatusHandlerTests
    {
        private readonly Mock<ILeadRepository> _mockLeadRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IEventStore> _mockEventStore;
        private readonly GetLeadsByStatusHandler _handler;

        public GetLeadsByStatusHandlerTests()
        {
            _mockLeadRepository = new Mock<ILeadRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockEventStore = new Mock<IEventStore>();
            _handler = new GetLeadsByStatusHandler(_mockLeadRepository.Object, _mockMapper.Object, _mockEventStore.Object);
        }

        [Fact]
        public async Task Handle_InvalidStatus_ThrowsArgumentException()
        {
            var query = new GetLeadsByStatusQuery("InvalidStatus");

            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ValidStatus_ReturnsListOfLeads()
        {
            var status = LeadStatus.Invited;
            var leads = new List<Lead>
        {
            new Lead("John", "Doe", "Suburb", "Category", "Description", 100, "john.doe@example.com", "1234567890")
        };
            var leadDtos = new List<LeadDetailsDto>
        {
            new LeadDetailsDto { Id = Guid.NewGuid(), ContactFirstName = "John", DateCreated = DateTime.UtcNow, Suburb = "Suburb", Category = "Category", Description = "Description", Price = 100, Status = LeadStatus.Invited, ContactFullName = "John Doe", ContactPhoneNumber = "1234567890", ContactEmail = "john.doe@example.com" }
        };
            var query = new GetLeadsByStatusQuery(status.ToString());
            _mockLeadRepository.Setup(repo => repo.GetByStatusAsync(status)).ReturnsAsync(leads);
            _mockMapper.Setup(m => m.Map<List<LeadDetailsDto>>(leads)).Returns(leadDtos);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(leadDtos, result);
        }
    }
}
