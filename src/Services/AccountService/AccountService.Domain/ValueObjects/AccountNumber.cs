namespace AccountService.Domain.ValueObjects;

public sealed record AccountNumber
{
    // Private constructor ensures that validation happens before setting the value
    private AccountNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Account number cannot be null or empty.", nameof(value));

        if (value.Length != 10) // Assuming the account number must be 10 digits long, for example
            throw new ArgumentException("Account number must be 10 digits long.", nameof(value));

        if (!IsDigitsOnly(value))
            throw new ArgumentException("Account number must contain only digits.", nameof(value));

        Value = value;
    }

    public string Value { get; }

    // Factory method to create an AccountNumber with validation
    public static AccountNumber From(string value)
    {
        return new AccountNumber(value);
    }

    public override string ToString()
    {
        return Value;
    }

    // Helper method to check if the string contains only digits
    private static bool IsDigitsOnly(string str)
    {
        foreach (var c in str)
            if (c < '0' || c > '9')
                return false;
        return true;
    }
}