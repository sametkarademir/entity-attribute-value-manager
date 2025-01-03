using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Values;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Application.Services;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Infrastructure.Abstracts;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Services;

public class ValueAppService(IValueRepository valueRepository) : ApplicationService, IValueAppService
{
    public async Task<ValueResponseDto> GetValueByIdAsync(Guid id)
    {
        var value = await valueRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (value == null)
        {
            throw new ArgumentException("Value not found");
        }
        
        return ObjectMapper.Map<Value, ValueResponseDto>(value);
    }

    public async Task<PaginatedResponseDto<ValueResponseDto>> GetValuesPaginatedAsync(PaginatedRequestDto request)
    {
        var values = await valueRepository.GetListAsync(
            index: request.PageIndex,
            size: request.PageSize
        );
        
        return ObjectMapper.Map<PaginatedResponseDto<ValueResponseDto>>(values);
    }

    public async Task<ValueResponseDto> CreateValueAsync(CreateValueRequestDto request)
    {
        var value = ObjectMapper.Map<CreateValueRequestDto, Value>(request);
        value = await valueRepository.AddAsync(value, true);
        
        return ObjectMapper.Map<Value, ValueResponseDto>(value);
    }

    public async Task<ValueResponseDto> UpdateValueAsync(Guid id, UpdateValueRequestDto request)
    {
        var value = await valueRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (value == null)
        {
            throw new ArgumentException("Value not found");
        }
        
        ObjectMapper.Map(request, value);
        value = await valueRepository.UpdateAsync(value, true);
        
        return ObjectMapper.Map<Value, ValueResponseDto>(value);
    }

    public async Task DeleteValueAsync(Guid id)
    {
        var value = await valueRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (value == null)
        {
            throw new ArgumentException("Value not found");
        }
        
        await valueRepository.DeleteAsync(value, true);
    }
}