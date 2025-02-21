using MediatR;
using Moq;
using MyApp.Application.Commands;
using MyApp.Application.Handlers;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Events;

namespace MyApp.Tests.Application.Handlers
{
    public class DeclineLeadHandlerTests
    {
        private readonly Mock<ILeadRepository> _mockLeadRepository;
        private readonly Mock<IEventStore> _mockEventStore;
        private readonly DeclineLeadHandler _handler;

        public DeclineLeadHandlerTests()
        {
            _mockLeadRepository = new Mock<ILeadRepository>();
            _mockEventStore = new Mock<IEventStore>();
            _handler = new DeclineLeadHandler(_mockLeadRepository.Object, _mockEventStore.Object);
        }

        [Fact]
        public async Task Handle_LeadNotFound_ThrowsKeyNotFoundException()
        {
            var command = new DeclineLeadCommand { LeadId = Guid.NewGuid() };
            _mockLeadRepository.Setup(repo => repo.GetByIdAsync(command.LeadId)).ReturnsAsync((Lead?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ValidLead_DeclinesLeadAndUpdatesRepository()
        {
            var lead = new Lead("Jane", "Doe", "jane.doe@example.com", "0987654321", "Another Company", 2000000M, "Referral", "Invited");
            lead.ClearDomainEvents();
            var command = new DeclineLeadCommand { LeadId = lead.Id };
            _mockLeadRepository.Setup(repo => repo.GetByIdAsync(command.LeadId)).ReturnsAsync((Lead?)lead);

            var result = await _handler.Handle(command, CancellationToken.None);

            _mockLeadRepository.Verify(repo => repo.UpdateAsync(lead), Times.Once);
            _mockEventStore.Verify(store => store.AppendAsync(It.IsAny<BaseDomainEvent>()), Times.AtLeastOnce);
            Assert.Equal(Unit.Value, result);
        }
    }
}
