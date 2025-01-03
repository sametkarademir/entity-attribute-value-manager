using SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Categories;
using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.CategoryAttributes;

public class CategoryAttributeResponseDto : AggregateRootDto
{
    public Guid CategoryId { get; set; }
    public CategoryResponseDto? Category { get; set; }
    
    public Guid AttributeId { get; set; }
    public AttributeResponseDto? Attribute { get; set; }
}