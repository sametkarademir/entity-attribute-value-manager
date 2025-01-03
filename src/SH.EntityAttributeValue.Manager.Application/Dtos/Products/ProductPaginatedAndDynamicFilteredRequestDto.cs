using SH.EntityAttributeValue.Manager.Application.Dtos.Dynamic;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Products;

public class ProductPaginatedAndDynamicFilteredRequestDto : PaginatedRequestDto
{
    public DynamicQuery DynamicQuery { get; set; } = null!;
}