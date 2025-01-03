using SH.EntityAttributeValue.Manager.Application.Dtos.Categories;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Application.Services;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Infrastructure.Abstracts;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Services;

public class CategoryAppService(ICategoryRepository categoryRepository) : ApplicationService, ICategoryAppService
{
    public async Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id)
    {
        var category = await categoryRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (category == null)
        {
            throw new ArgumentException("Category not found");
        }
        
        return ObjectMapper.Map<CategoryResponseDto>(category);
    }

    public async Task<PaginatedResponseDto<CategoryResponseDto>> GetCategoriesPaginatedAsync(PaginatedRequestDto request)
    {
        var categories = await categoryRepository.GetListAsync(
            orderBy: item => item.OrderBy(x => x.Name),
            index: request.PageIndex,
            size: request.PageSize
        );
        
        return ObjectMapper.Map<PaginatedResponseDto<CategoryResponseDto>>(categories);
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryRequestDto request)
    {
        var category = ObjectMapper.Map<CreateCategoryRequestDto, Category>(request);
        category = await categoryRepository.AddAsync(category, true);
        
        return ObjectMapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto> UpdateCategoryAsync(Guid id, UpdateCategoryRequestDto request)
    {
        var category = await categoryRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (category == null)
        {
            throw new ArgumentException("Category not found");
        }
        
        ObjectMapper.Map(request, category);
        category = await categoryRepository.UpdateAsync(category, true);
        
        return ObjectMapper.Map<CategoryResponseDto>(category);
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await categoryRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (category == null)
        {
            throw new ArgumentException("Category not found");
        }
        
        await categoryRepository.DeleteAsync(category, true);
    }
}