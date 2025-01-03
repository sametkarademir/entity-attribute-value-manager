using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Products;
using SH.EntityAttributeValue.Manager.Application.Services.Common;

namespace SH.EntityAttributeValue.Manager.Application.Services;

public interface IProductAppService : IScopedService
{
    Task<ProductResponseDto> GetProductByIdAsync(Guid id);
    Task<PaginatedResponseDto<ProductResponseDto>> GetProductsPaginatedAsync(PaginatedRequestDto request);
    Task<PaginatedResponseDto<ProductResponseDto>> GetProductsPaginatedAndDynamicFilteredAsync(ProductPaginatedAndDynamicFilteredRequestDto request);
    Task<PaginatedResponseDto<ProductResponseDto>> TestDynamicQueryAsync(ProductDynamicFilterRequestDto request);
    Task<ProductResponseDto> CreateProductAsync(CreateProductRequestDto request);
    Task<ProductResponseDto> UpdateProductAsync(Guid id, UpdateProductRequestDto request);
    Task DeleteProductAsync(Guid id);
}