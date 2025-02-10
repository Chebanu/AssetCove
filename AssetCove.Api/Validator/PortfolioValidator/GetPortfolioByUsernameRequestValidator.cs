using AssetCove.Api.Extensions;
using AssetCove.Contracts.Http.Portfolio.Requests;
using FluentValidation;

namespace AssetCove.Api.Validator.PortfolioValidator;

public class GetPortfolioByUsernameRequestValidator : AbstractValidator<GetUserPortfoliosRequest>
{
    public GetPortfolioByUsernameRequestValidator()
    {
        this.RuleForUsername(x => x.Username);
    }
}
