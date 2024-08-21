using AccountService.Domain.Entities;
using AccountService.Domain.ValueObjects;

namespace AccountService.Tests;

public class AccountTests
{
    [Fact]
    public void Constructor_ShouldCreateAccount_WithValidParameters()
    {
        var accountNumber = AccountNumber.From("1234567890");
        var customerId = CustomerId.From(Guid.NewGuid());

        var account = new Account(accountNumber, customerId);

        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.Equal(customerId, account.CustomerId);
        Assert.Equal(DateTime.UtcNow.Date, account.CreatedDate.Date);
    }

    [Fact]
    public void RehydrationConstructor_ShouldCreateAccount_FromPersistedData()
    {
        var accountId = AccountId.New();
        var accountNumber = AccountNumber.From("1234567890");
        var customerId = CustomerId.From(Guid.NewGuid());
        var createdDate = new DateTime(2024, 1, 1);

        var account = new Account(accountId, accountNumber, customerId, createdDate);

        Assert.Equal(accountId, account.Id);
        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.Equal(customerId, account.CustomerId);
        Assert.Equal(createdDate, account.CreatedDate);
    }
}