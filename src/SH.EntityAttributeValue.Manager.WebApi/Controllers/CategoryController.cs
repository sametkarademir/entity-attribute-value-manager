using Microsoft.AspNetCore.Mvc;
using SH.EntityAttributeValue.Manager.Application.Dtos.Categories;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Services;

namespace SH.EntityAttributeValue.Manager.WebApi.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryAppService _categoryAppService;

    public CategoryController(ICategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryAsync([FromRoute(Name = "id")] Guid id)
    {
        var result = await _categoryAppService.GetCategoryByIdAsync(id);
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategoriesPaginatedAsync([FromQuery] PaginatedRequestDto request)
    {
        var result = await _categoryAppService.GetCategoriesPaginatedAsync(request);
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequestDto request)
    {
        var result = await _categoryAppService.CreateCategoryAsync(request);
        
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryAsync([FromRoute(Name = "id")] Guid id, [FromBody] UpdateCategoryRequestDto request)
    {
        var result = await _categoryAppService.UpdateCategoryAsync(id, request);
        
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryAsync([FromRoute(Name = "id")] Guid id)
    {
        await _categoryAppService.DeleteCategoryAsync(id);
        
        return Ok();
    }
}