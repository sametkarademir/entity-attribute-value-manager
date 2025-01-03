using SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Services.Common;

namespace SH.EntityAttributeValue.Manager.Application.Services;

public interface IAttributeAppService : IScopedService
{
    Task<AttributeResponseDto> GetAttributeByIdAsync(Guid id);
    Task<PaginatedResponseDto<AttributeResponseDto>> GetAttributesPaginatedAsync(PaginatedRequestDto request);
    Task<AttributeResponseDto> CreateAttributeAsync(CreateAttributeRequestDto request);
    Task<AttributeResponseDto> UpdateAttributeAsync(Guid id, UpdateAttributeRequestDto request);
    Task DeleteAttributeAsync(Guid id);
}