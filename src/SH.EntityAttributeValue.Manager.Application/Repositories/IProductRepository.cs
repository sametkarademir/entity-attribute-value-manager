using SH.EntityAttributeValue.Manager.Application.Dtos.Paging;
using SH.EntityAttributeValue.Manager.Application.Dtos.Products;
using SH.EntityAttributeValue.Manager.Application.Repositories.Common;
using SH.EntityAttributeValue.Manager.Domain.Entities;

namespace SH.EntityAttributeValue.Manager.Application.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Paginate<Product>> EavFilterAsync(ProductEavFilterRequestDto request);
}