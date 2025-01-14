using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using Attribute = SH.EntityAttributeValue.Manager.Domain.Entities.Attribute;

namespace SH.EntityAttributeValue.Manager.Persistence.Contexts;

public class BaseDbContext : DbContext
{
    public DbSet<Attribute> Attributes { get; set; }
    public DbSet<AttributeOption> AttributeOptions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryAttribute> CategoryAttributes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Value> Values { get; set; }
    
    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseLoggerFactory(DbContextLoggerFactory.LoggerFactory)
                .EnableSensitiveDataLogging();
        }
    }
}

public static class DbContextLoggerFactory
{
    public static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
    {
        builder
            .AddConsole()
            .SetMinimumLevel(LogLevel.Information);
    });
}