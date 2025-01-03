using SH.EntityAttributeValue.Manager.Application.Dtos.Categories;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Services.Common;

namespace SH.EntityAttributeValue.Manager.Application.Services;

public interface ICategoryAppService : IScopedService
{
    Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id);
    Task<PaginatedResponseDto<CategoryResponseDto>> GetCategoriesPaginatedAsync(PaginatedRequestDto request);
    Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryRequestDto request);
    Task<CategoryResponseDto> UpdateCategoryAsync(Guid id, UpdateCategoryRequestDto request);
    Task DeleteCategoryAsync(Guid id);
}