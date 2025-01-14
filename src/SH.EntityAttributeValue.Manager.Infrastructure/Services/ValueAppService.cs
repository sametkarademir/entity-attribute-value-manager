using System.Globalization;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Values;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Application.Services;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Domain.Enums;
using SH.EntityAttributeValue.Manager.Infrastructure.Abstracts;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Services;

public class ValueAppService(IValueRepository valueRepository, IAttributeAppService attributeAppService) : ApplicationService, IValueAppService
{
    
    public async Task<ValueResponseDto> CreateValueAsync(CreateValueRequestDto request)
    {
        var attribute = await attributeAppService.GetAttributeByIdAsync(request.AttributeId);
        
        var value = new Value();
        value.Id = Guid.NewGuid();
        value.AttributeId = request.AttributeId;
        value.ProductId = request.ProductId;
        value.Content = request.Content;

        switch (attribute.DataType)
        {
            case DataTypes.String:
                value.AsString = request.Content;
                break;
            case DataTypes.Boolean:
                value.AsBoolean = bool.Parse(request.Content);
                break;
            case DataTypes.Date:
                value.AsDate = DateTime.Parse(request.Content);
                value.AsDate = value.AsDate.Value.ToUniversalTime();
                break;
            case DataTypes.Integer:
                value.AsInteger = int.Parse(request.Content);
                break;
            case DataTypes.Decimal:
                value.AsDecimal = Convert.ToDecimal(request.Content, CultureInfo.InvariantCulture);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        value = await valueRepository.AddAsync(value, true);
        
        return ObjectMapper.Map<Value, ValueResponseDto>(value);
    }

    public async Task DeleteValueAsync(Guid id)
    {
        var value = await valueRepository.GetAsync(
            predicate: item => item.Id == id
        );
        
        if (value == null)
        {
            throw new ArgumentException("Value not found");
        }
        
        await valueRepository.DeleteAsync(value, true);
    }
}