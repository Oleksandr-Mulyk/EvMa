using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Text.Json;
using EvMa.ServiceDefaults.Handlers;

namespace EvMa.ServiceDefaults.Tests.HandlersTests
{
    public class GlobalExceptionHandlerTests
    {
        [Fact]
        public async Task TryHandleAsync_LogsErrorAndReturnsProblemDetails()
        {
            // Arrange
            var logger = Substitute.For<ILogger<GlobalExceptionHandler>>();
            var handler = new GlobalExceptionHandler(logger);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var exception = new Exception("Test exception!");
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await handler.TryHandleAsync(context, exception, cancellationToken);

            // Assert
            Assert.True(result);

            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync(TestContext.Current.CancellationToken);
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBody);

            Assert.NotNull(problemDetails);
            Assert.Equal("Server Error", problemDetails.Title);
            Assert.Equal(500, problemDetails.Status);

            logger.ReceivedWithAnyArgs().Log(
                LogLevel.Error,
                Arg.Any<EventId>(),
                Arg.Any<object>(),
                exception,
                Arg.Any<Func<object, Exception?, string>>()
                );
        }
    }
}