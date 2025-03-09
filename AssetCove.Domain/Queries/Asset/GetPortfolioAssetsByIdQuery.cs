using AssetCove.Domain.Handlers;
using AssetCove.Domain.Repositories;

using Microsoft.Extensions.Logging;

using MediatR;

namespace AssetCove.Domain.Queries.Asset;

public class GetPortfolioAssetsByIdQuery : IRequest<GetPortfolioAssetsByIdResult>
{
    public Guid AssetId { get; init; }
    public string User { get; init; }
}

public class GetPortfolioAssetsByIdResult
{
    public Guid AssetId { get; init; }
    public string PortfolioName { get; init; }
    public string AssetName { get; init; }
    public string PortfolioOwner { get; init; }
    public decimal AssetAmount { get; init; }
    public bool Success { get; init; }
    public string[] Errors { get; init; }
}

public class GetPortfolioByIdQueryHandler : BaseRequestHandler<GetPortfolioAssetsByIdQuery, GetPortfolioAssetsByIdResult>
{
    private readonly IAssetCoveRepository _assetCoveRepository;

    public GetPortfolioByIdQueryHandler(ILogger<BaseRequestHandler<GetPortfolioAssetsByIdQuery, GetPortfolioAssetsByIdResult>> logger,
                                        IAssetCoveRepository assetCoveRepository) : base(logger)
    {
        _assetCoveRepository = assetCoveRepository;
    }

    protected override async Task<GetPortfolioAssetsByIdResult> HandleInternal(GetPortfolioAssetsByIdQuery request, CancellationToken cancellationToken)
    {
        var requestAsset = await _assetCoveRepository.GetUserAssetAsync(request.AssetId, request.User, cancellationToken);

        if(requestAsset == null)
        {
            return new GetPortfolioAssetsByIdResult
            {
                Success = false,
                Errors = ["The asset does not exist"]
            };
        }

        return new GetPortfolioAssetsByIdResult
        {
            Success = true
        };
    }
}