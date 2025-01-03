namespace SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

public abstract class AggregateRoot
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

public abstract class AggregateRootDto
{
    public Guid Id { get; set; }
}