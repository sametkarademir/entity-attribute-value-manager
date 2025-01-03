using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using SH.EntityAttributeValue.Manager.Persistence.Contexts;
using SH.EntityAttributeValue.Manager.Persistence.Repositories.Common;

namespace SH.EntityAttributeValue.Manager.Persistence.Repositories;

public class ValueRepository : EfRepositoryBase<Value, BaseDbContext>, IValueRepository
{
    public ValueRepository(BaseDbContext context) : base(context)
    {
    }
}