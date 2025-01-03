using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Domain.Entities;

public sealed class Product : AggregateRoot
{
    public string Name { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Value> Values { get; set; } = new List<Value>();
}