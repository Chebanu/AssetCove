using FluentValidation;

using AssetCove.Api.Extensions;
using AssetCove.Contracts.Http.Authentication;

namespace AssetCove.Api.Validator.AuthenticationValidator;

internal class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        this.RuleForUsername(x => x.Username);
        this.RuleForPassword(x => x.Password);
    }
}