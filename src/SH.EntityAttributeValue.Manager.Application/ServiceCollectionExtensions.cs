using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SH.EntityAttributeValue.Manager.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationExtensions(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        return services;
    }
}