using SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Application.Services;
using SH.EntityAttributeValue.Manager.Infrastructure.Abstracts;
using Attribute = SH.EntityAttributeValue.Manager.Domain.Entities.Attribute;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Services;

public class AttributeAppService(IAttributeRepository attributeRepository) : ApplicationService, IAttributeAppService
{
    public async Task<AttributeResponseDto> GetAttributeByIdAsync(Guid id)
    {
        var attribute = await attributeRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (attribute == null)
        {
            throw new ArgumentException("Attribute not found");
        }
        
        return ObjectMapper.Map<Attribute, AttributeResponseDto>(attribute);
    }

    public async Task<PaginatedResponseDto<AttributeResponseDto>> GetAttributesPaginatedAsync(
        PaginatedRequestDto request)
    {
        var attributes = await attributeRepository.GetListAsync(
            orderBy: item => item.OrderBy(x => x.Name),
            index: request.PageIndex,
            size: request.PageSize
        );
        
        return ObjectMapper.Map<PaginatedResponseDto<AttributeResponseDto>>(attributes);
    }

    public async Task<AttributeResponseDto> CreateAttributeAsync(CreateAttributeRequestDto request)
    {
        var attribute = ObjectMapper.Map<CreateAttributeRequestDto, Attribute>(request);
        attribute = await attributeRepository.AddAsync(attribute, true);
        
        return ObjectMapper.Map<Attribute, AttributeResponseDto>(attribute);
    }

    public async Task<AttributeResponseDto> UpdateAttributeAsync(Guid id, UpdateAttributeRequestDto request)
    {
        var attribute = await attributeRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (attribute == null)
        {
            throw new ArgumentException("Attribute not found");
        }
        
        ObjectMapper.Map(request, attribute);
        attribute = await attributeRepository.UpdateAsync(attribute, true);
        
        return ObjectMapper.Map<Attribute, AttributeResponseDto>(attribute);
    }

    public async Task DeleteAttributeAsync(Guid id)
    {
        var attribute = await attributeRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (attribute == null)
        {
            throw new ArgumentException("Attribute not found");
        }
        
        await attributeRepository.DeleteAsync(attribute, true);
    }
}