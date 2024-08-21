namespace AccountService.Domain.ValueObjects;

public sealed record FiservAcctId
{
    public string Value { get; }

    private FiservAcctId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Fiserv account ID cannot be null or empty.", nameof(value));
        }

        Value = value;
    }

    public static FiservAcctId From(string value)
    {
        return new FiservAcctId(value);
    }

    public override string ToString() => Value;
}
