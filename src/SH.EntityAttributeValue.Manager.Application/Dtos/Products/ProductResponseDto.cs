using SH.EntityAttributeValue.Manager.Application.Dtos.Categories;
using SH.EntityAttributeValue.Manager.Application.Dtos.Values;
using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Products;

public class ProductResponseDto : AggregateRootDto
{
    public string Name { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public CategoryResponseDto? Category { get; set; }
    
    public ICollection<ValueResponseDto> Values { get; set; } = new List<ValueResponseDto>();
}