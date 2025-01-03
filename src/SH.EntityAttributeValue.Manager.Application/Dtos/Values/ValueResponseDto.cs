using SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Products;
using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Values;

public class ValueResponseDto : AggregateRootDto
{
    public string Content { get; set; } = null!;
    
    public Guid ProductId { get; set; }
    public ProductResponseDto? Product { get; set; }

    public Guid AttributeId { get; set; }
    public AttributeResponseDto? Attribute { get; set; }
}