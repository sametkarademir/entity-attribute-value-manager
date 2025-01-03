using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Persistence.Contexts;
using SH.EntityAttributeValue.Manager.Persistence.Repositories.Common;
using Attribute = SH.EntityAttributeValue.Manager.Domain.Entities.Attribute;

namespace SH.EntityAttributeValue.Manager.Persistence.Repositories;

public class AttributeRepository : EfRepositoryBase<Attribute, BaseDbContext>, IAttributeRepository
{
    public AttributeRepository(BaseDbContext context) : base(context)
    {
    }
}