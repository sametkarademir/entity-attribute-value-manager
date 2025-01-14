using Microsoft.EntityFrameworkCore;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Products;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Application.Services;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Infrastructure.Abstracts;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Services;

public class ProductAppService(IProductRepository productRepository, IValueAppService valueAppService)
    : ApplicationService, IProductAppService
{
    public async Task<ProductResponseDto> GetProductByIdAsync(Guid id)
    {
        var product = await productRepository.GetAsync(
            predicate: item => item.Id == id,
            include: queryable => queryable
                .Include(include => include.Values)
                .ThenInclude(thenInclude => thenInclude.Attribute!)
                .Include(include => include.Category!)
        );

        if (product == null)
        {
            throw new ArgumentException("Product not found");
        }

        return ObjectMapper.Map<ProductResponseDto>(product);
    }

    public async Task<PaginatedResponseDto<ProductResponseDto>> GetProductsPaginatedAsync(PaginatedRequestDto request)
    {
        var products = await productRepository.GetListAsync(
            // predicate: predicate => 
            //     predicate.CategoryId == Guid.Parse("ca169144-f1da-4f84-97bb-c2687a701f6f") &&
            //     predicate.Values.Any(v => v.AttributeId == Guid.Parse("c3f4c4c6-d820-468e-9448-8afa0e81343e"))
            // ,
            orderBy: item => item.OrderBy(x => x.Name),
            include: queryable => queryable
                .Include(include => include.Values)
                .ThenInclude(thenInclude => thenInclude.Attribute!)
                .Include(include => include.Category!),
            index: request.PageIndex,
            size: request.PageSize
        );

        return ObjectMapper.Map<PaginatedResponseDto<ProductResponseDto>>(products);
    }

    public async Task<PaginatedResponseDto<ProductResponseDto>> GetProductsPaginatedAndDynamicFilteredAsync(
        ProductPaginatedAndDynamicFilteredRequestDto request)
    {
        var products = await productRepository.GetListByDynamicAsync(
            request.DynamicQuery,
            include: queryable => queryable
                .Include(include => include.Values)
                .ThenInclude(thenInclude => thenInclude.Attribute!)
                .Include(include => include.Category!),
            index: request.PageIndex,
            size: request.PageSize
        );
        
        return ObjectMapper.Map<PaginatedResponseDto<ProductResponseDto>>(products);
    }

    public async Task<PaginatedResponseDto<ProductResponseDto>> EavFilterAsync(ProductEavFilterRequestDto request)
    {
        var products = await productRepository.EavFilterAsync(request);
        
        return ObjectMapper.Map<PaginatedResponseDto<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto> CreateProductAsync(CreateProductRequestDto request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CategoryId = request.CategoryId
        };
        product = await productRepository.AddAsync(product, true);

        if (request.Values != null)
        {
            foreach (var value in request.Values)
            {
                value.ProductId = product.Id;
                await valueAppService.CreateValueAsync(value);
            }
        }
        
        return ObjectMapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> UpdateProductAsync(Guid id, UpdateProductRequestDto request)
    {
        var product = await productRepository.GetAsync(
            predicate: item => item.Id == id,
            include: queryable => queryable.Include(include => include.Values)
        );

        if (product == null)
        {
            throw new ArgumentException("Product not found");
        }
        
        product.Name = request.Name;
        product.CategoryId = request.CategoryId;

        product = await productRepository.UpdateAsync(product, true);
        
        foreach (var value in product.Values.ToList())
        {
            await valueAppService.DeleteValueAsync(value.Id);
        }

        if (request.Values != null)
        {
            foreach (var value in request.Values)
            {
                value.ProductId = product.Id;
                await valueAppService.CreateValueAsync(value);
            }
        }
        
        return ObjectMapper.Map<ProductResponseDto>(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await productRepository.GetAsync(
            predicate: item => item.Id == id
        );

        if (product == null)
        {
            throw new ArgumentException("Product not found");
        }

        await productRepository.DeleteAsync(product, true);
    }
}