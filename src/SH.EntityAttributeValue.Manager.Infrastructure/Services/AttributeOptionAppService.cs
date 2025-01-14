using SH.EntityAttributeValue.Manager.Application.Dtos.AttributeOptions;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Application.Services;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Infrastructure.Abstracts;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Services;

public class AttributeOptionAppService(IAttributeOptionRepository attributeOptionRepository) : ApplicationService, IAttributeOptionAppService
{
    public async Task<AttributeOptionResponseDto> CreateAttributeOptionAsync(CreateAttributeOptionRequestDto request)
    {
        var attributeOption = ObjectMapper.Map<CreateAttributeOptionRequestDto, AttributeOption>(request);
        attributeOption = await attributeOptionRepository.AddAsync(attributeOption, true);
        
        return ObjectMapper.Map<AttributeOption, AttributeOptionResponseDto>(attributeOption);
    }

    public async Task DeleteAttributeOptionAsync(Guid id)
    {
        var attributeOption = await attributeOptionRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (attributeOption == null)
        {
            throw new ArgumentException("Attribute option not found");
        }
        
        await attributeOptionRepository.DeleteAsync(attributeOption, true);
    }
}