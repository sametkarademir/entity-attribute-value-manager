namespace SH.EntityAttributeValue.Manager.Application.Repositories.Common;

public interface IQuery<T>
{
    IQueryable<T> Query();
}