namespace AccountService.Domain.ValueObjects;

public sealed record CustomerId
{
    private CustomerId(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("Customer ID cannot be an empty GUID.", nameof(value));

        Value = value;
    }

    public Guid Value { get; }

    public static CustomerId New()
    {
        return new CustomerId(Guid.NewGuid());
    }

    public static CustomerId From(Guid value)
    {
        return new CustomerId(value);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}