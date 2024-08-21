using AccountService.Domain.ValueObjects;
using SharedKernel.Domain.Entities;

namespace AccountService.Domain.Entities;

public class Account : Entity<AccountId>
{
    // Parameterless constructor for EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    private Account() : base(default!) { }
#pragma warning restore CS8618

    // Constructor for creating a new account
    public Account(AccountNumber accountNumber, CustomerId customerId)
        : base(AccountId.New())
    {
        AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
        CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        CreatedDate = DateTime.UtcNow; // TODO: Have interface handle this for deterministic testability
    }

    // Constructor for rehydrating from persistence
    public Account(AccountId accountId, AccountNumber accountNumber, CustomerId customerId, DateTime createdDate)
        : base(accountId)
    {
        AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
        CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        CreatedDate = createdDate;
    }

    public AccountNumber AccountNumber { get; private set; }
    public CustomerId CustomerId { get; private set; }
    // public FiservAcctId FiservAcctId { get; private set; } // TODO: External API integration
    public DateTime CreatedDate { get; private set; }

    // Domain logic can go here
}