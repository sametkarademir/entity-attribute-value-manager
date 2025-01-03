using SH.EntityAttributeValue.Manager.Application.Dtos.Values;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Products;

public class CreateProductRequestDto
{
    public string Name { get; set; } = null!;

    public Guid CategoryId { get; set; }

    public List<CreateValueRequestDto>? Values { get; set; }
}