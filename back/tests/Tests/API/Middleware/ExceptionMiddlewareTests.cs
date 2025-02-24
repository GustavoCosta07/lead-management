using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using MyApp.API.Middleware;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MyApp.Tests.API.Middleware
{
    public class ExceptionMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_ValidationException_ReturnsBadRequest()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var logger = new Mock<ILogger<ExceptionMiddleware>>();
            var middleware = new ExceptionMiddleware(next: (innerHttpContext) => throw new FluentValidation.ValidationException("Validation failed"), logger.Object);

            await middleware.InvokeAsync(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();
            var response = JsonSerializer.Deserialize<JsonElement>(responseBody);

            Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
            Assert.Equal("Erro de validação", response.GetProperty("message").GetString());
        }

        [Fact]
        public async Task InvokeAsync_GenericException_ReturnsInternalServerError()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var logger = new Mock<ILogger<ExceptionMiddleware>>();
            var middleware = new ExceptionMiddleware(next: (innerHttpContext) => throw new Exception("Generic error"), logger.Object);

            await middleware.InvokeAsync(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();
            var response = JsonSerializer.Deserialize<JsonElement>(responseBody);

            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
            Assert.Equal("Ocorreu um erro interno no servidor.", response.GetProperty("message").GetString());
        }
    }
}