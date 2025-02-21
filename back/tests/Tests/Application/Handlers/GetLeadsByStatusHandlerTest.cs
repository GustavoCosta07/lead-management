using Moq;
using MyApp.Application.Query;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Enums;

namespace MyApp.Tests.Application.Handlers
{
    public class GetLeadsByStatusHandlerTests
    {
        private readonly Mock<ILeadRepository> _mockLeadRepository;
        private readonly GetLeadsByStatusHandler _handler;

        public GetLeadsByStatusHandlerTests()
        {
            _mockLeadRepository = new Mock<ILeadRepository>();
            _handler = new GetLeadsByStatusHandler(_mockLeadRepository.Object);
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
                new Lead("John", "Doe", "Suburb", "Category", "Description", 1000000M, "john.doe@example.com", "1234567890")
            };
            var query = new GetLeadsByStatusQuery(status.ToString());
            _mockLeadRepository.Setup(repo => repo.GetByStatusAsync(status)).ReturnsAsync(leads);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(leads, result);
        }
    }
}
