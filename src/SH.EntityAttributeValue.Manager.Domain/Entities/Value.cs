using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Domain.Entities;

public sealed class Value : AggregateRoot
{
    public string Content { get; set; } = null!;
    
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid AttributeId { get; set; }
    public Attribute? Attribute { get; set; }
}