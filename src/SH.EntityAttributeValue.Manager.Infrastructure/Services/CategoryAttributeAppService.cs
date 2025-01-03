using SH.EntityAttributeValue.Manager.Application.Dtos.CategoryAttributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Application.Services;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Infrastructure.Abstracts;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Services;

public class CategoryAttributeAppService(ICategoryAttributeRepository categoryAttributeRepository) : ApplicationService, ICategoryAttributeAppService
{
    public async Task<PaginatedResponseDto<CategoryAttributeResponseDto>> GetCategoryAttributesPaginatedByCategoryIdAsync(Guid categoryId, PaginatedRequestDto request)
    {
        var categoryAttributes = await categoryAttributeRepository.GetListAsync(
            predicate: item => item.CategoryId == categoryId,
            index: request.PageIndex,
            size: request.PageSize
        );
        
        return ObjectMapper.Map<PaginatedResponseDto<CategoryAttributeResponseDto>>(categoryAttributes);
    }

    public async Task<PaginatedResponseDto<CategoryAttributeResponseDto>> GetCategoryAttributesPaginatedByAttributeIdAsync(Guid attributeId, PaginatedRequestDto request)
    {
        var categoryAttributes = await categoryAttributeRepository.GetListAsync(
            predicate: item => item.AttributeId == attributeId,
            index: request.PageIndex,
            size: request.PageSize
        );
        
        return ObjectMapper.Map<PaginatedResponseDto<CategoryAttributeResponseDto>>(categoryAttributes);
    }

    public async Task<CategoryAttributeResponseDto> AssignCategoryAttributeAsync(AssignCategoryAttributeRequestDto request)
    {
        var categoryAttribute = ObjectMapper.Map<AssignCategoryAttributeRequestDto, CategoryAttribute>(request);
        categoryAttribute = await categoryAttributeRepository.AddAsync(categoryAttribute, true);
        
        return ObjectMapper.Map<CategoryAttributeResponseDto>(categoryAttribute);
    }

    public async Task<CategoryAttributeResponseDto> UnAssignCategoryAttributeAsync(UnAssignCategoryAttributeRequestDto request)
    {
        var categoryAttribute = await categoryAttributeRepository.GetAsync(
            predicate: item => item.CategoryId == request.CategoryId && item.AttributeId == request.AttributeId
        );
        
        if (categoryAttribute == null)
        {
            throw new ArgumentException("CategoryAttribute not found");
        }
        
        categoryAttribute = await categoryAttributeRepository.DeleteAsync(categoryAttribute, true);
        
        return ObjectMapper.Map<CategoryAttributeResponseDto>(categoryAttribute);
    }
}