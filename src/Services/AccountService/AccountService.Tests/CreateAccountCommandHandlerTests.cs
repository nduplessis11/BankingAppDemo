using AccountService.Application.Features.Accounts.CreateAccount;
using AccountService.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Commands;

namespace AccountService.Tests;

public class CreateAccountCommandHandlerTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CommandDispatcher _commandDispatcher;

    public CreateAccountCommandHandlerTests()
    {
        // Setup DI container and register services
        var services = new ServiceCollection();
        services.AddScoped<ICommandHandler<CreateAccountCommand, CreateAccountResult>, CreateAccountCommandHandler>();
        services.AddScoped<CommandDispatcher>();

        _serviceProvider = services.BuildServiceProvider();
        _commandDispatcher = _serviceProvider.GetRequiredService<CommandDispatcher>();
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnCreateAccountResult_WithValidAccountId()
    {
        // Arrange
        var accountNumber = AccountNumber.From("1234567890");
        var customerId = CustomerId.From(Guid.NewGuid());
        var command = new CreateAccountCommand(accountNumber, customerId);

        // Act
        var result = await _commandDispatcher.DispatchAsync<CreateAccountCommand, CreateAccountResult>(command);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(AccountId.Empty.Value, result.AccountId.Value);
    }
}