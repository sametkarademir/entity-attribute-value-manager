using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SH.EntityAttributeValue.Manager.Application;
using SH.EntityAttributeValue.Manager.Infrastructure;
using SH.EntityAttributeValue.Manager.Infrastructure.Extensions;
using SH.EntityAttributeValue.Manager.Persistence;
using SH.EntityAttributeValue.Manager.WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

#region AddCors
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", corsPolicyBuilder =>
        corsPolicyBuilder.SetIsOriginAllowedToAllowWildcardSubdomains()
            .WithOrigins(allowedOrigins ?? new string[] { "http://localhost:3000" })
            .AllowAnyHeader()
            .AllowAnyMethod());
});
#endregion

#region AddLayerServiceExtensions
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceExtensions(builder.Configuration);
builder.Services.AddApplicationExtensions();
#endregion

#region ApplicationConfigurations
builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
#endregion

#region AddSwaggerConfigurations
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition(
        name: "Bearer",
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer YOUR_TOKEN\". \r\n\r\n"
                + "`Enter your token in the text input below.`"
        }
    );
    opt.OperationFilter<BearerSecurityRequirementOperationFilter>();
});
#endregion

var app = builder.Build();

ServiceResolverExtension.Configure(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseCors("MyCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();