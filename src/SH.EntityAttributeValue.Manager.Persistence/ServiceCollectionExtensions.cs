using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SH.EntityAttributeValue.Manager.Application.Repositories;
using SH.EntityAttributeValue.Manager.Infrastructure.Extensions;
using SH.EntityAttributeValue.Manager.Persistence.Contexts;
using SH.EntityAttributeValue.Manager.Persistence.Repositories;

namespace SH.EntityAttributeValue.Manager.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAttributeRepository, AttributeRepository>();
        services.AddScoped<IAttributeOptionRepository, AttributeOptionRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryAttributeRepository, CategoryAttributeRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IValueRepository, ValueRepository>();
        
        services.AddDbContextFactory<BaseDbContext>(opt => 
            opt.UseNpgsql(configuration.GetPostgresqlConnectionString()), ServiceLifetime.Scoped);
        
        return services;
    }
}