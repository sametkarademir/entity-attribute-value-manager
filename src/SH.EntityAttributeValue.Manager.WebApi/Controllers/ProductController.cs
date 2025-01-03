using Microsoft.AspNetCore.Mvc;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Products;
using SH.EntityAttributeValue.Manager.Application.Services;

namespace SH.EntityAttributeValue.Manager.WebApi.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IProductAppService _productAppService;

    public ProductController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductAsync([FromRoute(Name = "id")] Guid id)
    {
        var result = await _productAppService.GetProductByIdAsync(id);
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProductsPaginatedAsync([FromQuery] PaginatedRequestDto request)
    {
        var result = await _productAppService.GetProductsPaginatedAsync(request);
        
        return Ok(result);
    }
    
    [HttpPost("dynamic")]
    public async Task<IActionResult> GetProductsPaginatedAndDynamicFilteredAsync([FromBody] ProductPaginatedAndDynamicFilteredRequestDto request)
    {
        var result = await _productAppService.GetProductsPaginatedAndDynamicFilteredAsync(request);
        
        return Ok(result);
    }
    
    [HttpPost("test")]
    public async Task<IActionResult> TestDynamicQueryAsync([FromBody] ProductDynamicFilterRequestDto request)
    {
        var result = await _productAppService.TestDynamicQueryAsync(request);
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequestDto request)
    {
        var result = await _productAppService.CreateProductAsync(request);
        
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductAsync([FromRoute(Name = "id")] Guid id, [FromBody] UpdateProductRequestDto request)
    {
        var result = await _productAppService.UpdateProductAsync(id, request);
        
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync([FromRoute(Name = "id")] Guid id)
    {
        await _productAppService.DeleteProductAsync(id);
        
        return Ok();
    }
}