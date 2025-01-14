namespace SH.EntityAttributeValue.Manager.Application.Dtos.AttributeOptions;

public class CreateAttributeOptionRequestDto
{
    public string Value { get; set; } = null!;
    public Guid AttributeId { get; set; }
}