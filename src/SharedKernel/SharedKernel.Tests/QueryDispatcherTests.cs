using Microsoft.Extensions.DependencyInjection;
using Moq;
using SharedKernel.Application.Queries;

namespace SharedKernel.Tests;

public class QueryDispatcherTests
{
    [Fact]
    public async Task QueryDispatcher_ShouldInvokeQueryHandlerAndReturnResult()
    {
        // Arrange
        var query = new Mock<IQuery<string>>();
        var cancellationToken = CancellationToken.None;
        var expectedResponse = "Hello";

        var handlerMock = new Mock<IQueryHandler<IQuery<string>, string>>();
        handlerMock.Setup(x => x.HandleAsync(query.Object, cancellationToken)).ReturnsAsync(expectedResponse);

        var services = new ServiceCollection();
        services.AddSingleton(handlerMock.Object);
        var serviceProvider = services.BuildServiceProvider();

        var dispatcher = new QueryDispatcher(serviceProvider);

        // Act
        var response = await dispatcher.DispatchAsync<IQuery<string>, string>(query.Object, cancellationToken);

        // Assert
        Assert.Equal(expectedResponse, response);
        handlerMock.Verify(x => x.HandleAsync(query.Object, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task QueryDispatcher_ShouldThrow_WhenNoHandlerIsRegistered()
    {
        // Arrange
        var query = new Mock<IQuery<string>>();
        var cancellationToken = CancellationToken.None;
        var serviceProvider = new ServiceCollection().BuildServiceProvider();
        var dispatcher = new QueryDispatcher(serviceProvider);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            dispatcher.DispatchAsync<IQuery<string>, string>(query.Object, cancellationToken));
    }
}