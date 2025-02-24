using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using MyApp.Application.Behaviors;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MyApp.Tests.Application.Behaviors
{
    public class ValidationBehaviorTests
    {
        [Fact]
        public async Task Handle_ValidRequest_DoesNotThrowException()
        {
            var validators = new List<IValidator<TestRequest>>
            {
                new TestRequestValidator()
            };
            var behavior = new ValidationBehavior<TestRequest, TestResponse>(validators);
            var request = new TestRequest { Name = "Valid Name" };

            var result = await behavior.Handle(request, () => Task.FromResult(new TestResponse()), CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            var validators = new List<IValidator<TestRequest>>
            {
                new TestRequestValidator()
            };
            var behavior = new ValidationBehavior<TestRequest, TestResponse>(validators);
            var request = new TestRequest { Name = "" };

            await Assert.ThrowsAsync<ValidationException>(() => behavior.Handle(request, () => Task.FromResult(new TestResponse()), CancellationToken.None));
        }

        private class TestRequest : IRequest<TestResponse>
        {
            public string Name { get; set; }
        }

        private class TestResponse
        {
        }

        private class TestRequestValidator : AbstractValidator<TestRequest>
        {
            public TestRequestValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }
    }
}