using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Domain.Entities;

public sealed class CategoryAttribute : AggregateRoot
{
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public Guid AttributeId { get; set; }
    public Attribute? Attribute { get; set; }
}