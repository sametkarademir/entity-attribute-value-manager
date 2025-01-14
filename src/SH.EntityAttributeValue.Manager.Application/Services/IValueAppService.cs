using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Values;
using SH.EntityAttributeValue.Manager.Application.Services.Common;

namespace SH.EntityAttributeValue.Manager.Application.Services;

public interface IValueAppService : IScopedService
{
    Task<ValueResponseDto> CreateValueAsync(CreateValueRequestDto request);
    Task DeleteValueAsync(Guid id);
}