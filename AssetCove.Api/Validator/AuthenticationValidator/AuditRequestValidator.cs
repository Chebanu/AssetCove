using FluentValidation;

using AssetCove.Contracts.Http;

namespace AssetCove.Api.Validator.AuthenticationValidator;

public class AuditRequestValidator : AbstractValidator<AuditRequest>
{
    public AuditRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}