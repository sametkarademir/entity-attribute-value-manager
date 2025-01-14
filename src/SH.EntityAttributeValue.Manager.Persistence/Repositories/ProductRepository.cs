using Microsoft.EntityFrameworkCore;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Products;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Persistence.Contexts;
using SH.EntityAttributeValue.Manager.Persistence.Extensions;
using SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy;
using SH.EntityAttributeValue.Manager.Persistence.Repositories.Common;

namespace SH.EntityAttributeValue.Manager.Persistence.Repositories;

public class ProductRepository : EfRepositoryBase<Product, BaseDbContext>, IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }
    
    public async Task<Paginate<Product>> EavFilterAsync(ProductEavFilterRequestDto request)
    {
        var queryable = Query();

        if (request.CategoryId != Guid.Empty)
        {
            queryable = queryable.Where(x => x.CategoryId == request.CategoryId);
        }

        if (request.Filters != null && request.Filters.Any())
        {
            queryable = queryable.ApplyEavFilter(request.Filters.Select(x => new AttributeFilter
            {
                AttributeId = x.AttributeId,
                Content = x.Content,
                DataType = x.DataType,
                OperatorType = x.OperatorType,
                LogicType = x.LogicType
            }).ToList());
        }
        
        queryable = queryable
            .Include(include => include.Category)
            .Include(include => include.Values)
            .ThenInclude(thenInclude => thenInclude.Attribute);
        
        return await queryable.ToPaginateAsync(request.PageIndex, request.PageSize);
    }
}