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
        var fiservAcctId = FiservAcctId.From("10183903051613");

        var account = new Account(accountNumber, customerId, fiservAcctId);

        Assert.NotNull(account.Id);
        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.Equal(customerId, account.CustomerId);
        Assert.Equal(DateTime.UtcNow.Date, account.CreatedDate.Date);
        Assert.Equal(fiservAcctId, account.FiservAcctId);
    }

    [Fact]
    public void RehydrationConstructor_ShouldCreateAccount_FromPersistedData()
    {
        var accountId = AccountId.New();
        var accountNumber = AccountNumber.From("1234567890");
        var customerId = CustomerId.From(Guid.NewGuid());
        var createdDate = new DateTime(2024, 1, 1);
        var fiservAcctId = FiservAcctId.From("10183903051613");

        var account = new Account(accountId, accountNumber, customerId, createdDate, fiservAcctId);

        Assert.Equal(accountId, account.Id);
        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.Equal(customerId, account.CustomerId);
        Assert.Equal(createdDate, account.CreatedDate);
        Assert.Equal(fiservAcctId, account.FiservAcctId);
    }
}