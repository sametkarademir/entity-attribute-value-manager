using Microsoft.Extensions.Configuration;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string GetPostgresqlConnectionString(this IConfiguration configuration) =>
        configuration["ConnectionStrings:Postgresql"] ?? "Host=localhost;Port=5432;Database=SH_EntityAttributeValue_Manager;Username=postgres;Password=postgres";
}