using Moq;
using MyApp.Application.Query;
using MyApp.Application.Handlers;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Tests.Application.Handlers
{
    public class GetLeadsHandlerTests
    {
        private readonly Mock<ILeadRepository> _mockLeadRepository;
        private readonly GetLeadsHandler _handler;

        public GetLeadsHandlerTests()
        {
            _mockLeadRepository = new Mock<ILeadRepository>();
            _handler = new GetLeadsHandler(_mockLeadRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsListOfLeads()
        {
            var leads = new List<Lead>
            {
                new Lead("John", "Doe", "Suburb", "Category", "Description", 1000000M, "john.doe@example.com", "1234567890"),
                new Lead("Jane", "Doe", "Suburb", "Category", "Description", 2000000M, "jane.doe@example.com", "0987654321")
            };
            _mockLeadRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(leads);
            var query = new GetLeadsQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(leads, result);
        }
    }
}
