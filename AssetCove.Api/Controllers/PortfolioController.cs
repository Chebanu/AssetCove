using AssetCove.Contracts.Http;
using AssetCove.Contracts.Models;
using AssetCove.Contracts.Http.Portfolio.Requests;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using AssetCove.Contracts.Http.Portfolio.Responses;
using AssetCove.Domain.Queries;
using AssetCove.Domain.Commands.Portfolios;


namespace AssetCove.Api.Controllers;

[Route("portfolio")]
public class PortfolioController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreatePortfolioRequest> _createPortfolioValidator;
    private readonly IValidator<GetUserPortfoliosRequest> _getPortfoliosValidator;
    private readonly IValidator<UpdatePortfolioNameRequest> _updatePortfolioNameValidator;

    public PortfolioController(IMediator mediator, IValidator<CreatePortfolioRequest> createPortfolioValidator,
                                                    IValidator<GetUserPortfoliosRequest> getPortfolioByIdValidator,
        IValidator<UpdatePortfolioNameRequest> updatePortfolioNameValidator)
    {
        _mediator = mediator;
        _createPortfolioValidator = createPortfolioValidator;
        _getPortfoliosValidator = getPortfolioByIdValidator;
        _updatePortfolioNameValidator = updatePortfolioNameValidator;
    }

    [HttpGet]
    [Route("user/{username}/portfolios")]
    [Authorize]
    public async Task<IActionResult> GetPortfoliosByUsername([FromRoute] string username, CancellationToken cancellationToken = default)
    {
        var request = new GetUserPortfoliosRequest { Username = username };

        var validationResult = await _getPortfoliosValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
            });
        }

        var portfoliosRequest = new GetUserPortfoliosQuery
        {
            User = User.Identity.Name,
            Owner = username
        };

        var portfoliosResult = await _mediator.Send(portfoliosRequest, cancellationToken);

        if (!portfoliosResult.Success)
        {
            return NotFound(new ErrorResponse
            {
                Errors = portfoliosResult.Errors
            });
        }

        var response = portfoliosResult.UserPortfolioResults.Select(p => new GetPortfolioResponse
        {
            PortfolioId = p.PortfolioId,
            PortfolioName = p.PortfolioName
        }).ToList();

        return Ok(response);
    }


    [HttpGet]
    [Route("{portfolioId}")]
    [Authorize]
    public async Task<IActionResult> GetPortfolioById([FromRoute] Guid portfolioId, CancellationToken cancellationToken = default)
    {
        var request = new GetPortfolioByIdQuery
        {
            PortfolioId = portfolioId,
            User = User.Identity.Name
        };

        var portfoliosResult = await _mediator.Send(request, cancellationToken);

        if (!portfoliosResult.Success)
        {
            return NotFound(new ErrorResponse
            {
                Errors = portfoliosResult.Errors
            });
        }

        return Ok(new GetPortfolioResponse
        {
            PortfolioId = portfolioId,
            PortfolioName = portfoliosResult.PortfolioName,
            PortfolioOwner = portfoliosResult.PortfolioOwner
        });
    }

    [HttpPost]
    [Route("")]
    [Authorize]
    public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioRequest portfolioRequest,
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
    public async Task<IActionResult> UpdatePortfolioName([FromBody] UpdatePortfolioNameRequest portfolioUpdateRequest,
                                                            CancellationToken cancellationToken = default)
    {
        var validationResult = await _updatePortfolioNameValidator.ValidateAsync(portfolioUpdateRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray()
            });
        }

        var updatePortfolio = new UpdatePortfolioNameCommand
        {
            PortfolioId = portfolioUpdateRequest.PortfolioId,
            UpdatedName = portfolioUpdateRequest.Name,
            User = User.Identity.Name,
        };


        var portfolioResult = await _mediator.Send(updatePortfolio, cancellationToken);

        return !portfolioResult.Success ?
            BadRequest(new ErrorResponse
            {
                Errors = portfolioResult.Errors
            })
            :
            NoContent();
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