namespace AccountService.Domain.ValueObjects;

public readonly record struct AccountId(Guid Value)
{
    public static AccountId Empty => default;
    public static AccountId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}