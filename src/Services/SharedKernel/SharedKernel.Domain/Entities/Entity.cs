namespace SharedKernel.Domain.Entities;

public abstract class Entity<TId>(TId id)
    where TId : IEquatable<TId>
{
    public TId Id { get; private set; } = id;
}
