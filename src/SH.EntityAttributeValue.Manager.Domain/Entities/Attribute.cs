using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;
using SH.EntityAttributeValue.Manager.Domain.Enums;

namespace SH.EntityAttributeValue.Manager.Domain.Entities;

public sealed class Attribute : AggregateRoot
{
    public string Name { get; set; } = null!;
    public DataTypes DataType { get; set; }
    public bool IsMultiple { get; set; }
    
    public ICollection<CategoryAttribute> CategoryAttributes { get; set; } = new List<CategoryAttribute>();
    public ICollection<AttributeOption> AttributeOptions { get; set; } = new List<AttributeOption>();
}