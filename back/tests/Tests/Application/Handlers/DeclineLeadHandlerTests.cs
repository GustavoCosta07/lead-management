using MediatR;
using Moq;
using MyApp.Application.Commands;
using MyApp.Application.Handlers;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Events;
using MyApp.Domain.Exceptions;
using Xunit;
using MyApp.Application.Interfaces;

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
        public async Task Handle_LeadNotFound_ThrowsAppException()
        {
            var command = new DeclineLeadCommand(Guid.NewGuid());
            _mockLeadRepository.Setup(repo => repo.GetByIdAsync(command.LeadId)).ReturnsAsync((Lead?)null);

            await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ValidLead_DeclinesLeadAndUpdatesRepository()
        {
            var lead = new Lead("Jane", "Jane Smith", "Los Angeles", "Cleaning", "Clean the office", 200, "vendas@test.com", "987-654-3210");
            var command = new DeclineLeadCommand(lead.Id);
            _mockLeadRepository.Setup(repo => repo.GetByIdAsync(command.LeadId)).ReturnsAsync(lead);

            var result = await _handler.Handle(command, CancellationToken.None);

            _mockLeadRepository.Verify(repo => repo.UpdateAsync(lead), Times.Once);
            _mockEventStore.Verify(store => store.AppendAsync(It.IsAny<BaseDomainEvent>()), Times.AtLeastOnce);
            Assert.Equal(Unit.Value, result);
        }
    }
}