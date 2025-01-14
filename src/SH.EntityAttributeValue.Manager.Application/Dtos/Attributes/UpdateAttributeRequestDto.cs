using SH.EntityAttributeValue.Manager.Application.Dtos.AttributeOptions;
using SH.EntityAttributeValue.Manager.Domain.Enums;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;

public class UpdateAttributeRequestDto
{
    public string Name { get; set; } = null!;
    public DataTypes DataType { get; set; }
    public bool IsMultiple { get; set; }
    public List<CreateAttributeOptionRequestDto>? AttributeOptions { get; set; }
}