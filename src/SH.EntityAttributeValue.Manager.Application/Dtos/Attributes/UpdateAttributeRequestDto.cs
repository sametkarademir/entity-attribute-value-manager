using SH.EntityAttributeValue.Manager.Domain.Enums;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;

public class UpdateAttributeRequestDto
{
    public string Name { get; set; } = null!;
    public DataTypes DataType { get; set; }
}