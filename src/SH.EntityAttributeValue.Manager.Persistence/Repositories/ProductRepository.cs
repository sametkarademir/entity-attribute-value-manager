using Microsoft.EntityFrameworkCore;
using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Products;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Persistence.Contexts;
using SH.EntityAttributeValue.Manager.Persistence.Extensions;
using SH.EntityAttributeValue.Manager.Persistence.Repositories.Common;

namespace SH.EntityAttributeValue.Manager.Persistence.Repositories;

public class ProductRepository : EfRepositoryBase<Product, BaseDbContext>, IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<Paginate<Product>> TestDynamicQueryAsync(ProductDynamicFilterRequestDto request)
    {
        var queryable = Query();
        
        if (request.CategoryId != Guid.Empty)
        {
            queryable = queryable.Where(x => x.CategoryId == request.CategoryId);
        }

        queryable = queryable.ApplyDynamicFilter(request.Attributes);

        queryable = queryable
            .Include(include => include.Values)
            .ThenInclude(thenInclude => thenInclude.Attribute!)
            .Include(include => include.Category!);

        return await queryable.ToPaginateAsync(request.PageIndex, request.PageSize);
    }
}