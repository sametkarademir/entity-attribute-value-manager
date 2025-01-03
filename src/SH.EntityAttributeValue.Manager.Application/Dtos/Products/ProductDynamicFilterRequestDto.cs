using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Enums;
using SH.EntityAttributeValue.Manager.Domain.Enums;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Products;

public class ProductDynamicFilterRequestDto : PaginatedRequestDto
{
    public Guid CategoryId { get; set; }
    public List<FilterAttributeDto> Attributes { get; set; } = new();
}

public class FilterAttributeDto
{
    public Guid AttributeId { get; set; }
    public string Content { get; set; } = null!;
    public OperatorTypes OperatorType { get; set; }
    public LogicTypes LogicType { get; set; }
}