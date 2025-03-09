using AssetCove.Domain.Commands.Portfolios;
using AssetCove.Domain.Configuration;
using AssetCove.Domain.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace AssetCove.Domain;

public static class DomainServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services,
        string connectionString,
        IConfiguration jwt)
    {
        services
            .AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<AssetCoveDbContext>()
            .AddDefaultTokenProviders();

        return services
        .AddOptions()
            .Configure<JwtSettings>(jwt)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePortfolioCommand>())
            .AddDbContext<AssetCoveDbContext>(options => options.UseSqlServer(connectionString));
    }
}