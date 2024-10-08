using AccountService.Domain.ValueObjects;
using SharedKernel.Domain.Entities;

namespace AccountService.Domain.Entities;

public class Account : Entity<AccountId>
{
    // Debatable to have infrastructure concerns in domain entities, but it's a common practice with EF
    // There are ways to avoid this, but it's more complex

    // Parameterless constructor for EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    private Account() : base(default!) { }
#pragma warning restore CS8618

    // Constructor for creating a new account
    public Account(AccountNumber accountNumber, CustomerId customerId, FiservAcctId fiservAcctId)
        : base(AccountId.New())
    {
        AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
        CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        FiservAcctId = fiservAcctId ?? throw new ArgumentNullException(nameof(fiservAcctId));
        CreatedDate = DateTime.UtcNow; // TODO: Have interface handle this for deterministic testability
    }

    // Constructor for rehydrating from persistence
    public Account(AccountId accountId, AccountNumber accountNumber, CustomerId customerId, FiservAcctId fiservAcctId, DateTime createdDate)
        : base(accountId)
    {
        AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
        CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        FiservAcctId = fiservAcctId ?? throw new ArgumentNullException(nameof(fiservAcctId));
        CreatedDate = createdDate;
    }

    public AccountNumber AccountNumber { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public FiservAcctId FiservAcctId { get; private set; } // TODO: External API integration
    public DateTime CreatedDate { get; private set; }

    // Domain logic can go here
}