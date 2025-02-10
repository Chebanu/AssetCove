using FluentValidation;
using AssetCove.Contracts.Http;
using AssetCove.Contracts.Http.Authentication;
using AssetCove.Api.Validator.AuthenticationValidator;
using AssetCove.Api.Validator.PortfolioValidator;
using AssetCove.Contracts.Http.Portfolio.Requests;

namespace AssetCove.Api.StartupExtensions;

public static class ServiceValidatorConfiguration
{
    public static IServiceCollection AddValidatorConfiguration(this IServiceCollection services)
    {
        return services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserRequestValidator>()
                        .AddScoped<IValidator<AuthenticateUserRequest>, AuthenticateUserRequestValidator>()
                        .AddScoped<IValidator<AdminUpdateUserRequest>, AdminUpdateUserRequestValidator>()
                        .AddScoped<IValidator<PortfolioCreateRequest>, PortfolioCreateRequestValidator>()
                        .AddScoped<IValidator<GetUserPortfoliosRequest>, GetPortfolioByUsernameRequestValidator>()
                        .AddScoped<IValidator<AuditRequest>, AuditRequestValidator>();
    }
}