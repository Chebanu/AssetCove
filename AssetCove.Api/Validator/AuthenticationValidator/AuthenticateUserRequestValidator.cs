using FluentValidation;

using AssetCove.Api.Extensions;
using AssetCove.Contracts.Http.Authentication;

internal class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
{
    public AuthenticateUserRequestValidator()
    {
        this.RuleForUsername(x => x.Username);
        this.RuleForPassword(x => x.Password);
    }
}