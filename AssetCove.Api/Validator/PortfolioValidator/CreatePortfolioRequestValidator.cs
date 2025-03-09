using FluentValidation;

using AssetCove.Api.Extensions;
using AssetCove.Contracts.Http.Portfolio.Requests;

namespace AssetCove.Api.Validator.PortfolioValidator;

public class CreatePortfolioRequestValidator : AbstractValidator<CreatePortfolioRequest>
{
    public CreatePortfolioRequestValidator()
    {
        RuleFor(x => x.PortfolioName)
            .NotNull()
            .WithMessage("The field is null")
            .Length(1, 100)
            .WithMessage("Your portfolio name must be in the 1-100 character range");

        RuleFor(x => x.Visibility)
            .NotNull()
            .IsInEnum();

        this.RuleForShareableUserList(x => x.ShareableList);
    }
}