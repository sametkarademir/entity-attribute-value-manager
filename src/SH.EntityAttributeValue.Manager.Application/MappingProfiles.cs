using AutoMapper;
using SH.EntityAttributeValue.Manager.Application.Dtos.AttributeOptions;
using SH.EntityAttributeValue.Manager.Application.Dtos.Attributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Categories;
using SH.EntityAttributeValue.Manager.Application.Dtos.CategoryAttributes;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Products;
using SH.EntityAttributeValue.Manager.Application.Dtos.Values;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using Attribute = SH.EntityAttributeValue.Manager.Domain.Entities.Attribute;

namespace SH.EntityAttributeValue.Manager.Application;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Paginate<Attribute>, PaginatedResponseDto<AttributeResponseDto>>().ReverseMap();
        CreateMap<Attribute, AttributeResponseDto>().ReverseMap();
        CreateMap<Attribute, CreateAttributeRequestDto>().ReverseMap();
        CreateMap<Attribute, UpdateAttributeRequestDto>().ReverseMap();
        
        CreateMap<Paginate<AttributeOption>, PaginatedResponseDto<AttributeOptionResponseDto>>().ReverseMap();
        CreateMap<AttributeOption, AttributeOptionResponseDto>().ReverseMap();
        CreateMap<AttributeOption, CreateAttributeOptionRequestDto>().ReverseMap();
        CreateMap<AttributeOption, UpdateAttributeOptionRequestDto>().ReverseMap();
        
        CreateMap<Paginate<Category>, PaginatedResponseDto<CategoryResponseDto>>().ReverseMap();
        CreateMap<Category, CategoryResponseDto>().ReverseMap();
        CreateMap<Category, CreateCategoryRequestDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryRequestDto>().ReverseMap();
        
        CreateMap<Paginate<CategoryAttribute>, PaginatedResponseDto<CategoryAttributeResponseDto>>().ReverseMap();
        CreateMap<CategoryAttribute, CategoryAttributeResponseDto>().ReverseMap();
        CreateMap<CategoryAttribute, AssignCategoryAttributeRequestDto>().ReverseMap();
        CreateMap<CategoryAttribute, UnAssignCategoryAttributeRequestDto>().ReverseMap();
        
        CreateMap<Paginate<Product>, PaginatedResponseDto<ProductResponseDto>>().ReverseMap();
        CreateMap<Product, ProductResponseDto>().ReverseMap();
        CreateMap<Product, CreateProductRequestDto>().ReverseMap();
        CreateMap<Product, UpdateProductRequestDto>().ReverseMap();
        
        CreateMap<Paginate<Value>, PaginatedResponseDto<ValueResponseDto>>().ReverseMap();
        CreateMap<Value, ValueResponseDto>().ReverseMap();
        CreateMap<Value, CreateValueRequestDto>().ReverseMap();
        CreateMap<Value, UpdateValueRequestDto>().ReverseMap();
    }
}