using AssetCove.Api.Constants;
using AssetCove.Api.StartupExtensions;
using AssetCove.Domain;
using AssetCove.Domain.Constants;
using AssetCove.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AssetCove",
        Description = "AssetCove",
    });

    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
});

builder.Services.AddControllers();

builder.Services.AddDomainServices(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    builder.Configuration.GetSection("Jwt")
);

builder.Services
            .AddAuthorizationBuilder()
            .AddPolicy(AuthorizePolicies.Admin, policy => policy.RequireClaim(ClaimTypes.Role, Roles.Admin))
            .AddPolicy(AuthorizePolicies.User, policy => policy.RequireClaim(ClaimTypes.Role, Roles.User));


builder.Services.AddAuthenticationService(builder.Configuration);

builder.Services.AddValidatorConfiguration();

//builder.Services.AddOptions().AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePortfolioCommand>());

builder.Services.AddScoped<IAssetCoveRepository, AssetCoveRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

public partial class Program { }