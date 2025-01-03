using Microsoft.AspNetCore.Mvc;
using SH.EntityAttributeValue.Manager.Application.Dtos.CategoryAttributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Services;

namespace SH.EntityAttributeValue.Manager.WebApi.Controllers;

[ApiController]
[Route("api/v1/category-attributes")]
public class CategoryAttributeController : ControllerBase
{
    private readonly ICategoryAttributeAppService _categoryAttributeAppService;

    public CategoryAttributeController(ICategoryAttributeAppService categoryAttributeAppService)
    {
        _categoryAttributeAppService = categoryAttributeAppService;
    }
    
    [HttpGet("category/{id}")]
    public async Task<IActionResult> GetCategoryAttributesPaginatedByCategoryIdAsync([FromRoute(Name = "id")] Guid id, [FromQuery] PaginatedRequestDto request)
    {
        var result = await _categoryAttributeAppService.GetCategoryAttributesPaginatedByCategoryIdAsync(id, request);
        
        return Ok(result);
    }
    
    [HttpGet("attribute/{id}")]
    public async Task<IActionResult> GetCategoryAttributesPaginatedByAttributeIdAsync([FromRoute(Name = "id")] Guid id, [FromQuery] PaginatedRequestDto request)
    {
        var result = await _categoryAttributeAppService.GetCategoryAttributesPaginatedByAttributeIdAsync(id, request);
        
        return Ok(result);
    }
    
    [HttpPost("assign")]
    public async Task<IActionResult> AssignCategoryAttributeAsync([FromBody] AssignCategoryAttributeRequestDto request)
    {
        var result = await _categoryAttributeAppService.AssignCategoryAttributeAsync(request);
        
        return Ok(result);
    }
    
    [HttpPost("unassign")]
    public async Task<IActionResult> UnAssignCategoryAttributeAsync([FromBody] UnAssignCategoryAttributeRequestDto request)
    {
        var result = await _categoryAttributeAppService.UnAssignCategoryAttributeAsync(request);
        
        return Ok(result);
    }
}