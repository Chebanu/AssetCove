using AssetCove.Contracts.Http;
using AssetCove.Contracts.Models;
using AssetCove.Contracts.Http.Portfolio;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using AssetCove.Domain.Commands;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using Azure.Core;


namespace AssetCove.Api.Controllers;

[Route("portfolio")]
public class PortfolioController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<PortfolioCreateRequest> _createPortfolioValidator;

    public PortfolioController(IMediator mediator, IValidator<PortfolioCreateRequest> createPortfolioValidator)
    {
        _mediator = mediator;
        _createPortfolioValidator = createPortfolioValidator;
    }

    [HttpGet]
    [Route("{portfolioId}")]
    [Authorize]
    public async Task<IActionResult> GetPortfolioId([FromRoute] Guid portfolioId, CancellationToken cancellationToken = default)
    {

        return Ok();
    }

    [HttpPost]
    [Route("")]
    [Authorize]
    public async Task<IActionResult> CreatePortfolio([FromBody] PortfolioCreateRequest portfolioRequest,
                                                        CancellationToken cancellationToken = default)
    {
        var validationResult = await _createPortfolioValidator.ValidateAsync(portfolioRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
            });
        }

        if ((portfolioRequest.Visibility == Visibility.Shared && portfolioRequest.ShareableList == null) ||
                (portfolioRequest.ShareableList != null && portfolioRequest.Visibility != Visibility.Shared))
        {
            return BadRequest(new ErrorResponse
            {
                Errors = [
                    portfolioRequest.Visibility == Visibility.Shared
                ? "With 'Shared' visibility the shared list should not be empty"
                : "You have to pick 'Shared' visibility to share with the others"
                ]
            });
        }

        var createPortfolio = new CreatePortfolioCommand
        {
            User = User.Identity.Name,
            PortfolioName = portfolioRequest.PortfolioName,
            Visibility = portfolioRequest.Visibility,
            ShareableList = portfolioRequest.Visibility switch
            {
                Visibility.Public => null,
                Visibility.Private => null,
                Visibility.Shared => portfolioRequest.ShareableList,
                _ => throw new NotImplementedException()
            }
        };

        CreatePortfolioResult portfolioResult = await _mediator.Send(createPortfolio, cancellationToken);

        return !portfolioResult.Success ?
            BadRequest(new ErrorResponse
            {
                Errors = portfolioResult.Errors
            }) :
            Created($"portfolios/{portfolioResult.PortfolioId}", new CreatePortfolioResponse
            {
                PortfolioId = portfolioResult.PortfolioId,
                Success = portfolioResult.Success
            });
    }

    [HttpPut]
    [Route("")]
    [Authorize]
    public async Task<IActionResult> UpdatePortfolioName([FromBody] PortfolioUpdateNameRequest portfolioUpdateRequest, CancellationToken cancellationToken = default)
    {
       //valiadation



        return Ok();
    }

    [HttpPut]
    [Route("remove")]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio([FromBody] DeletePortfolioRequest deletePortfolioRequest, CancellationToken cancellationToken = default)
    {
        //validation

        return Ok();
    }
}