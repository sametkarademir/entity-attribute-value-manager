using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Domain.Entities;

public class AttributeOption : AggregateRoot
{
    public string Value { get; set; } = null!;
    
    public Guid AttributeId { get; set; }
    public Attribute? Attribute { get; set; }
}