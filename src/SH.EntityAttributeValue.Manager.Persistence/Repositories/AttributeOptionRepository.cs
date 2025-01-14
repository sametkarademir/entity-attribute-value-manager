using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Persistence.Contexts;
using SH.EntityAttributeValue.Manager.Persistence.Repositories.Common;

namespace SH.EntityAttributeValue.Manager.Persistence.Repositories;

public class AttributeOptionRepository : EfRepositoryBase<AttributeOption, BaseDbContext>, IAttributeOptionRepository
{
    public AttributeOptionRepository(BaseDbContext context) : base(context)
    {
    }
}