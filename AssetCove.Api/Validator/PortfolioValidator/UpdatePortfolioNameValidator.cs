using FluentValidation;

using AssetCove.Contracts.Http.Portfolio.Requests;

namespace AssetCove.Api.Validator.PortfolioValidator;

public class UpdatePortfolioNameValidator : AbstractValidator<UpdatePortfolioNameRequest>
{
    public UpdatePortfolioNameValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("The field is null")
            .Length(1, 100)
            .WithMessage("Your portfolio name must be in the 1-100 character range");
    }
}