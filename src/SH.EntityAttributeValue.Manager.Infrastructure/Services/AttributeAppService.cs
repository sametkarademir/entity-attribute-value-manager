using Microsoft.EntityFrameworkCore;
using SH.EntityAttributeValue.Manager.Application.Dtos.AttributeOptions;
using SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Application.Services;
using SH.EntityAttributeValue.Manager.Infrastructure.Abstracts;
using Attribute = SH.EntityAttributeValue.Manager.Domain.Entities.Attribute;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Services;

public class AttributeAppService(IAttributeRepository attributeRepository, IAttributeOptionAppService attributeOptionAppService) : ApplicationService, IAttributeAppService
{
    public async Task<AttributeResponseDto> GetAttributeByIdAsync(Guid id)
    {
        var attribute = await attributeRepository.GetAsync(
            predicate: item => item.Id == id,
            include: item => item.Include(x => x.AttributeOptions)
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
            include: item => item.Include(x => x.AttributeOptions),
            index: request.PageIndex,
            size: request.PageSize
        );
        
        return ObjectMapper.Map<PaginatedResponseDto<AttributeResponseDto>>(attributes);
    }

    public async Task<AttributeResponseDto> CreateAttributeAsync(CreateAttributeRequestDto request)
    {
        var attribute = new Attribute
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            DataType = request.DataType,
            IsMultiple = request.IsMultiple
        };
        
        attribute = await attributeRepository.AddAsync(attribute, true);

        if (attribute.IsMultiple && request.AttributeOptions != null)
        {
            foreach (var attributeOption in request.AttributeOptions)
            {
                await attributeOptionAppService.CreateAttributeOptionAsync(new CreateAttributeOptionRequestDto
                {
                    Value = attributeOption.Value,
                    AttributeId = attribute.Id
                });
            }
        }
        
        return ObjectMapper.Map<Attribute, AttributeResponseDto>(attribute);
    }

    public async Task<AttributeResponseDto> UpdateAttributeAsync(Guid id, UpdateAttributeRequestDto request)
    {
        var attribute = await attributeRepository.GetAsync(
            predicate: item => item.Id == id,
            include: item => item.Include(x => x.AttributeOptions)
        );
        
        if (attribute == null)
        {
            throw new ArgumentException("Attribute not found");
        }
        
        attribute.Name = request.Name;
        attribute.DataType = request.DataType;
        attribute.IsMultiple = request.IsMultiple;
        attribute = await attributeRepository.UpdateAsync(attribute, true);
        
        if (attribute.IsMultiple && request.AttributeOptions != null)
        {
            foreach (var attributeOption in attribute.AttributeOptions)
            {
                await attributeOptionAppService.DeleteAttributeOptionAsync(attributeOption.Id);
            }
            
            foreach (var attributeOption in request.AttributeOptions)
            {
                await attributeOptionAppService.CreateAttributeOptionAsync(new CreateAttributeOptionRequestDto
                {
                    Value = attributeOption.Value,
                    AttributeId = attribute.Id
                });
            }
        }
        
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