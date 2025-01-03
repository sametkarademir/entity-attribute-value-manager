using Microsoft.AspNetCore.Mvc;
using SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Services;

namespace SH.EntityAttributeValue.Manager.WebApi.Controllers;

[ApiController]
[Route("api/v1/attributes")]
public class AttributeController : ControllerBase
{
    private readonly IAttributeAppService _attributeAppService;

    public AttributeController(IAttributeAppService attributeAppService)
    {
        _attributeAppService = attributeAppService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAttributeAsync([FromRoute(Name = "id")] Guid id)
    {
        var result = await _attributeAppService.GetAttributeByIdAsync(id);
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAttributesPaginatedAsync([FromQuery] PaginatedRequestDto request)
    {
        var result = await _attributeAppService.GetAttributesPaginatedAsync(request);
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAttributeAsync([FromBody] CreateAttributeRequestDto request)
    {
        var result = await _attributeAppService.CreateAttributeAsync(request);
        
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttributeAsync([FromRoute(Name = "id")] Guid id, [FromBody] UpdateAttributeRequestDto request)
    {
        var result = await _attributeAppService.UpdateAttributeAsync(id, request);
        
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttributeAsync([FromRoute(Name = "id")] Guid id)
    {
        await _attributeAppService.DeleteAttributeAsync(id);
        
        return Ok();
    }
}