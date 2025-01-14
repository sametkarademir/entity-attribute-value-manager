using SH.EntityAttributeValue.Manager.Application.Dtos.AttributeOptions;
using SH.EntityAttributeValue.Manager.Domain.AggrigateRoots;
using SH.EntityAttributeValue.Manager.Domain.Enums;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;

public class AttributeResponseDto : AggregateRootDto
{
    public string Name { get; set; } = null!;
    public DataTypes DataType { get; set; }
    public bool IsMultiple { get; set; }

    public List<AttributeOptionResponseDto> AttributeOptions { get; set; } = new();
}