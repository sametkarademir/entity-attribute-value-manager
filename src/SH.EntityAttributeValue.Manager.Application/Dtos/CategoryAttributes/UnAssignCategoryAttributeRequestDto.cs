namespace SH.EntityAttributeValue.Manager.Application.Dtos.CategoryAttributes;

public class UnAssignCategoryAttributeRequestDto
{
    public Guid CategoryId { get; set; }
    public Guid AttributeId { get; set; }
}