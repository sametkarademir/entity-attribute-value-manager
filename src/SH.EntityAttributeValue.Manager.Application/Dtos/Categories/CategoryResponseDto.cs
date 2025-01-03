using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Categories;

public class CategoryResponseDto : AggregateRootDto
{
    public string Name { get; set; } = null!;
}