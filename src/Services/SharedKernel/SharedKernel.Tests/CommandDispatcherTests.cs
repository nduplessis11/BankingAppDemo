using Microsoft.Extensions.DependencyInjection;
using Moq;
using SharedKernel.Application.Commands;

namespace SharedKernel.Tests;

public class CommandDispatcherTests
{
    [Fact]
    public async Task DispatchAsync_ShouldInvokeCommandHandler_WhenCommandIsSentWithNoResponse()
    {
        // Arrange
        var command = new Mock<ICommand>();
        var cancellationToken = CancellationToken.None;

        var handlerMock = new Mock<ICommandHandler<ICommand>>();
        handlerMock.Setup(x => x.HandleAsync(command.Object, cancellationToken)).Returns(Task.CompletedTask);

        var services = new ServiceCollection();
        services.AddSingleton(handlerMock.Object);
        var serviceProvider = services.BuildServiceProvider();

        var dispatcher = new CommandDispatcher(serviceProvider);

        // Act
        await dispatcher.DispatchAsync(command.Object, cancellationToken);

        // Assert
        handlerMock.Verify(x => x.HandleAsync(command.Object, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task DispatchAsync_ShouldInvokeCommandHandler_WhenCommandIsSentWithResponse()
    {
        // Arrange
        var command = new Mock<ICommand<Guid>>();
        var cancellationToken = CancellationToken.None;
        var expectedResponse = Guid.NewGuid();

        var handlerMock = new Mock<ICommandHandler<ICommand<Guid>, Guid>>();
        handlerMock.Setup(x => x.HandleAsync(command.Object, cancellationToken))
            .Returns(Task.FromResult(expectedResponse));

        var services = new ServiceCollection();
        services.AddSingleton(handlerMock.Object);
        var serviceProvider = services.BuildServiceProvider();

        var dispatcher = new CommandDispatcher(serviceProvider);

        // Act
        var response = await dispatcher.DispatchAsync<ICommand<Guid>, Guid>(command.Object, cancellationToken);

        // Assert
        Assert.Equal(expectedResponse, response);
        handlerMock.Verify(x => x.HandleAsync(command.Object, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task DispatchAsync_ShouldThrow_WhenNoCommandHandlerIsRegistered()
    {
        // Arrange
        var command = new Mock<ICommand>();
        var cancellationToken = CancellationToken.None;
        var serviceProvider = new ServiceCollection().BuildServiceProvider();
        var dispatcher = new CommandDispatcher(serviceProvider);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            dispatcher.DispatchAsync(command.Object, cancellationToken));
    }
}