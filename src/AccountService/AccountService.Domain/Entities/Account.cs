using AccountService.Domain.ValueObjects;

namespace AccountService.Domain.Entities;

public class Account
{
    // Constructor for creating a new account
    public Account(AccountNumber accountNumber, CustomerId customerId, Balance initialBalance, decimal overdraftLimit)
    {
        AccountId = AccountId.New();
        AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
        CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        Balance = initialBalance ?? throw new ArgumentNullException(nameof(initialBalance));
        OverdraftLimit = overdraftLimit;
        CreatedDate = DateTime.UtcNow; // TODO: Have interface handle this for deterministic testability
    }

    // Constructor for rehydrating from persistence
    public Account(AccountId accountId, AccountNumber accountNumber, CustomerId customerId, Balance balance,
        decimal overdraftLimit, DateTime createdDate)
    {
        AccountId = accountId;
        AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
        CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        Balance = balance ?? throw new ArgumentNullException(nameof(balance));
        OverdraftLimit = overdraftLimit;
        CreatedDate = createdDate;
    }

    public AccountId AccountId { get; private set; }
    public AccountNumber AccountNumber { get; private set; }
    public Balance Balance { get; private set; }
    public decimal OverdraftLimit { get; }
    public CustomerId CustomerId { get; private set; }
    public DateTime CreatedDate { get; private set; }

    // Methods for domain operations
    public void Deposit(decimal amount)
    {
        Balance = Balance.Add(amount);
    }

    public void Withdraw(decimal amount)
    {
        Balance = Balance.Subtract(amount);
    }

    public bool IsOverdrawn()
    {
        return Balance.IsOverdrawn(OverdraftLimit);
    }
}