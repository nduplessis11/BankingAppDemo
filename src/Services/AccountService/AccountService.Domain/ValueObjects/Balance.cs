namespace AccountService.Domain.ValueObjects;

public sealed record Balance
{
    private Balance(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static Balance From(decimal value)
    {
        return new Balance(value);
    }

    public Balance Add(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount to add must be greater than zero.", nameof(amount));

        return new Balance(Value + amount);
    }

    public Balance Subtract(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount to subtract must be greater than zero.", nameof(amount));

        return new Balance(Value - amount);
    }

    public bool IsOverdrawn(decimal overdraftLimit)
    {
        return Value < -overdraftLimit;
    }

    public override string ToString()
    {
        return Value.ToString("C"); // Format as currency
    }
}