using SH.EntityAttributeValue.Manager.Application.Dtos.CategoryAttributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Services.Common;

namespace SH.EntityAttributeValue.Manager.Application.Services;

public interface ICategoryAttributeAppService : IScopedService
{
    Task<PaginatedResponseDto<CategoryAttributeResponseDto>> GetCategoryAttributesPaginatedByCategoryIdAsync(Guid categoryId, PaginatedRequestDto request);
    Task<PaginatedResponseDto<CategoryAttributeResponseDto>> GetCategoryAttributesPaginatedByAttributeIdAsync(Guid attributeId, PaginatedRequestDto request);
    Task<CategoryAttributeResponseDto> AssignCategoryAttributeAsync(AssignCategoryAttributeRequestDto request);
    Task<CategoryAttributeResponseDto> UnAssignCategoryAttributeAsync(UnAssignCategoryAttributeRequestDto request);
}