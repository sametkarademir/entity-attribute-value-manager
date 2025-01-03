using Microsoft.Extensions.DependencyInjection;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Extensions;

public static class ServiceResolverExtension
{
    private static IServiceProvider? _serviceProvider;
    public static void Configure(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public static T Resolve<T>()
    {
        if (_serviceProvider == null)
        {
            throw new InvalidOperationException("Service provider not configured!");
        }

        return _serviceProvider.GetRequiredService<T>();
    }
    public static T ResolveScoped<T>() where T : notnull
    {
        if (_serviceProvider == null)
        {
            throw new InvalidOperationException("Service provider not configured!");
        }
        
        using var scope = _serviceProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }
    
    public static async Task<T> ResolveScopedAsync<T>() where T : notnull
    {
        if (_serviceProvider == null)
        {
            throw new InvalidOperationException("Service provider not configured!");
        }
        
        await using var scope = _serviceProvider.CreateAsyncScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }
}
