using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Domain.Entities;

public sealed class Value : AggregateRoot
{
    public string Content { get; set; } = null!;
    
    public string? AsString { get; set; }
    public bool? AsBoolean { get; set; }
    public DateTime? AsDate { get; set; }
    public int? AsInteger { get; set; }
    public decimal? AsDecimal { get; set; }
    
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid AttributeId { get; set; }
    public Attribute? Attribute { get; set; }
}