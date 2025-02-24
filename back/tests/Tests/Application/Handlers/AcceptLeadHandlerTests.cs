using MediatR;
using Moq;
using MyApp.Application.Commands;
using MyApp.Application.Handlers;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Domain.Events;
using MyApp.Domain.Exceptions;
using Xunit;

namespace MyApp.Tests.Application.Handlers
{
    public class AcceptLeadHandlerTests
    {
        private readonly Mock<ILeadRepository> _mockLeadRepository;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IEventStore> _mockEventStore;
        private readonly AcceptLeadHandler _handler;

        public AcceptLeadHandlerTests()
        {
            _mockLeadRepository = new Mock<ILeadRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockEventStore = new Mock<IEventStore>();
            _handler = new AcceptLeadHandler(_mockLeadRepository.Object, _mockEmailService.Object, _mockEventStore.Object);
        }

        [Fact]
        public async Task Handle_LeadNotFound_ThrowsAppException()
        {
            var command = new AcceptLeadCommand(Guid.NewGuid(), null, null, null);
            _mockLeadRepository.Setup(repo => repo.GetByIdAsync(command.LeadId)).ReturnsAsync((Lead?)null);

            await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ValidLead_AcceptsLeadAndSendsEmail()
        {
            var lead = new Lead("John", "John Doe", "New York", "Painting", "Paint the house", 600, "vendas@test.com", "123-456-7890");
            var command = new AcceptLeadCommand(lead.Id, "John Doe", "123-456-7890", "vendas@test.com");
            _mockLeadRepository.Setup(repo => repo.GetByIdAsync(command.LeadId)).ReturnsAsync(lead);

            var result = await _handler.Handle(command, CancellationToken.None);

            _mockLeadRepository.Verify(repo => repo.UpdateAsync(lead), Times.Once);
            _mockEmailService.Verify(service => service.SendEmailAsync("vendas@test.com", "Lead Aceito", It.IsAny<string>()), Times.Once);
            _mockEventStore.Verify(store => store.AppendAsync(It.IsAny<BaseDomainEvent>()), Times.AtLeastOnce);
            Assert.Equal(Unit.Value, result);
        }
    }
}