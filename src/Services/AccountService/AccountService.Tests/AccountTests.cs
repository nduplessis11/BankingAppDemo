using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;

namespace AccountService.Tests;

public class AccountTests
{
    [Fact]
    public void Constructor_ShouldCreateAccount_WithValidParameters()
    {
        // Arrange
        var accountNumber = AccountNumber.From("1234567890");
        var customerId = CustomerId.From(Guid.NewGuid());
        var initialBalance = Balance.From(1000m);
        const decimal overdraftLimit = 50m;

        // Act
        var account = new Account(accountNumber, customerId, initialBalance, overdraftLimit);

        // Assert
        Assert.NotNull(account.AccountId);
        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.Equal(customerId, account.CustomerId);
        Assert.Equal(initialBalance, account.Balance);
        Assert.False(account.IsOverdrawn());
    }

    [Fact]
    public void RehydrationConstructor_ShouldCreateAccount_FromPersistedData()
    {
        // Arrange
        var accountId = AccountId.New();
        var accountNumber = AccountNumber.From("1234567890");
        var customerId = CustomerId.From(Guid.NewGuid());
        var balance = Balance.From(500m);
        const decimal overdraftLimit = 50m;
        var createdDate = new DateTime(2024, 1, 1);

        // Act
        var account = new Account(accountId, accountNumber, customerId, balance, overdraftLimit, createdDate);

        // Assert
        Assert.Equal(accountId, account.AccountId);
        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.Equal(customerId, account.CustomerId);
        Assert.Equal(balance, account.Balance);
        Assert.Equal(createdDate, account.CreatedDate);
    }

    [Fact]
    public void Deposit_ShouldIncreaseBalance()
    {
        // Arrange
        var accountNumber = AccountNumber.From("1234567890");
        var customerId = CustomerId.From(Guid.NewGuid());
        var initialBalance = Balance.From(1000m);
        const decimal overdraftLimit = 50m;
        var account = new Account(accountNumber, customerId, initialBalance, overdraftLimit);

        // Act
        account.Deposit(200m);

        // Assert
        Assert.Equal(Balance.From(1200m), account.Balance);
    }

    [Fact]
    public void Withdraw_ShouldDecreaseBalance()
    {
        // Arrange
        var accountNumber = AccountNumber.From("1234567890");
        var customerId = CustomerId.From(Guid.NewGuid());
        var initialBalance = Balance.From(1000m);
        const decimal overdraftLimit = 50m;
        var account = new Account(accountNumber, customerId, initialBalance, overdraftLimit);

        // Act
        account.Withdraw(300m);

        // Assert
        Assert.Equal(Balance.From(700m), account.Balance);
    }

    [Fact]
    public void IsOverdrawn_ShouldReturnTrue_WhenBalanceIsNegative()
    {
        // Arrange
        var accountNumber = AccountNumber.From("1234567890");
        var customerId = CustomerId.From(Guid.NewGuid());
        var initialBalance = Balance.From(0m);
        const decimal overdraftLimit = 50m;
        var account = new Account(accountNumber, customerId, initialBalance, overdraftLimit);

        // Act
        account.Withdraw(100m);

        // Assert
        Assert.True(account.IsOverdrawn());
    }
}