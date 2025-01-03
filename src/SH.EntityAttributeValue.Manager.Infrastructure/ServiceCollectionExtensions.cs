using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SH.EntityAttributeValue.Manager.Application.Services.Common;

namespace SH.EntityAttributeValue.Manager.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Register Services Dynamic

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && !type.IsAbstract)
            .Where(type => typeof(IScopedService).IsAssignableFrom(type) ||
                           typeof(ITransientService).IsAssignableFrom(type) ||
                           typeof(ISingletonService).IsAssignableFrom(type));

        foreach (var type in types)
        {
            if (typeof(IScopedService).IsAssignableFrom(type))
            {
                RegisterService(services, type, ServiceLifetime.Scoped);
            }

            if (typeof(ITransientService).IsAssignableFrom(type))
            {
                RegisterService(services, type, ServiceLifetime.Transient);
            }

            if (typeof(ISingletonService).IsAssignableFrom(type))
            {
                RegisterService(services, type, ServiceLifetime.Singleton);
            }
        }

        #endregion

        return services;
    }

    private static void RegisterService(IServiceCollection services, Type implementationType, ServiceLifetime lifetime)
    {
        var interfaceType = implementationType.GetInterfaces()
            .FirstOrDefault(i =>
                i != typeof(IScopedService) && i != typeof(ITransientService) && i != typeof(ISingletonService));

        if (interfaceType != null)
        {
            services.Add(new ServiceDescriptor(interfaceType, implementationType, lifetime));
        }
        else
        {
            services.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));
        }
    }
}