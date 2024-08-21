namespace AccountService.Domain.ValueObjects;

public sealed record AccountId(Guid Value)
{
    public static AccountId New()
    {
        return new AccountId(Guid.NewGuid());
    }

    public static AccountId From(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("Account ID cannot be empty.", nameof(value));

        return new AccountId(value);
    }

    public static AccountId Empty => new(Guid.Empty);

    public override string ToString()
    {
        return Value.ToString();
    }
}