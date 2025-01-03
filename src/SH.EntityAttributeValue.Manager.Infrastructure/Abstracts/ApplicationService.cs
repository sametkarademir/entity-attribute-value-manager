using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SH.EntityAttributeValue.Manager.Infrastructure.Extensions;

namespace SH.EntityAttributeValue.Manager.Infrastructure.Abstracts;

public abstract class ApplicationService
{
    private readonly Lazy<ILogger> _logger;
    private readonly Lazy<ILoggerFactory> _loggerFactory;
    private readonly Lazy<IHttpContextAccessor> _httpContextAccessor;
    private readonly Lazy<IConfiguration> _configuration;
    
    private readonly Lazy<IMapper> _objectMapper;

    protected ApplicationService()
    {
        _loggerFactory = new Lazy<ILoggerFactory>(GetScopedService<ILoggerFactory>);
        _logger = new Lazy<ILogger>(() => _loggerFactory.Value.CreateLogger(GetType()));
        _httpContextAccessor = new Lazy<IHttpContextAccessor>(GetScopedService<IHttpContextAccessor>);
        _configuration = new Lazy<IConfiguration>(GetScopedService<IConfiguration>);
        
        _objectMapper = new Lazy<IMapper>(GetScopedService<IMapper>);
    }
    
    private ILoggerFactory LoggerFactory => _loggerFactory.Value;
    protected ILogger Logger => _logger.Value;
    protected ClaimsPrincipal CurrentUser => _httpContextAccessor.Value.HttpContext!.User;
    protected IHttpContextAccessor HttpContextAccessor => _httpContextAccessor.Value;
    protected IConfiguration Configuration => _configuration.Value;
    
    protected IMapper ObjectMapper => _objectMapper.Value;

    
    #region ServiceResolverExtension
    protected T GetScopedService<T>() where T : notnull
    {
        return ServiceResolverExtension.ResolveScoped<T>();
    }
    
    protected async Task<T> GetScopedServiceAsync<T>() where T : notnull
    {
        return await ServiceResolverExtension.ResolveScopedAsync<T>();
    }
    #endregion
}