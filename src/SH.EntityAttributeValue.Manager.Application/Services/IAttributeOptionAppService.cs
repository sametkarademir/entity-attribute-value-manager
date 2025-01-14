using SH.EntityAttributeValue.Manager.Application.Dtos.AttributeOptions;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Services.Common;

namespace SH.EntityAttributeValue.Manager.Application.Services;

public interface IAttributeOptionAppService : IScopedService
{
    Task<AttributeOptionResponseDto> CreateAttributeOptionAsync(CreateAttributeOptionRequestDto request);
    Task DeleteAttributeOptionAsync(Guid id);
}