using AssetCove.Contracts.Http;
using AssetCove.Contracts.Http.Asset.Request;
using AssetCove.Domain.Queries.Asset;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetCove.Api.Controllers;

[Route("assets")]
public class UserAssetController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserAssetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPortfolioAssets(GetPortfolioAssetsRequest getPortfolioAssetsRequest, CancellationToken cancellationToken)
    {
        var getPortfolioAssetsByIdQuery = new GetPortfolioAssetsByIdQuery
        {
            AssetId = getPortfolioAssetsRequest.AssetId,
            User = User.Identity.Name
        };

        var assetResult = await _mediator.Send(getPortfolioAssetsByIdQuery, cancellationToken);

        return !assetResult.Success ? BadRequest(new ErrorResponse
        {
            Errors = assetResult.Errors
        }) : Ok();
    }
}
