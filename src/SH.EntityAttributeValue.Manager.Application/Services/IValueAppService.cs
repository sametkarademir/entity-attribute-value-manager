using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Values;
using SH.EntityAttributeValue.Manager.Application.Services.Common;

namespace SH.EntityAttributeValue.Manager.Application.Services;

public interface IValueAppService : IScopedService
{
    Task<ValueResponseDto> GetValueByIdAsync(Guid id);
    Task<PaginatedResponseDto<ValueResponseDto>> GetValuesPaginatedAsync(PaginatedRequestDto request);
    Task<ValueResponseDto> CreateValueAsync(CreateValueRequestDto request);
    Task<ValueResponseDto> UpdateValueAsync(Guid id, UpdateValueRequestDto request);
    Task DeleteValueAsync(Guid id);
}