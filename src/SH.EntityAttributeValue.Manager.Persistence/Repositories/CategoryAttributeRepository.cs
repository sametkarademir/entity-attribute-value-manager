using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Persistence.Contexts;
using SH.EntityAttributeValue.Manager.Persistence.Repositories.Common;

namespace SH.EntityAttributeValue.Manager.Persistence.Repositories;

public class CategoryAttributeRepository : EfRepositoryBase<CategoryAttribute, BaseDbContext>, ICategoryAttributeRepository
{
    public CategoryAttributeRepository(BaseDbContext context) : base(context)
    {
    }
}