namespace SH.EntityAttributeValue.Manager.Application.Dtos.AttributeOptions;

public class UpdateAttributeOptionRequestDto
{
    public string Value { get; set; } = null!;
    public Guid AttributeId { get; set; }
}