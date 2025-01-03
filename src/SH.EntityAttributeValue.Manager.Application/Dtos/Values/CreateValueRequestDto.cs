namespace SH.EntityAttributeValue.Manager.Application.Dtos.Values;

public class CreateValueRequestDto
{
    public string Content { get; set; } = null!;
    
    public Guid ProductId { get; set; }
    public Guid AttributeId { get; set; }
}