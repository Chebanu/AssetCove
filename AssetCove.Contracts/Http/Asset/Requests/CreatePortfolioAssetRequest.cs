namespace AssetCove.Contracts.Http.Asset.Requests;

public class CreatePortfolioAssetRequest
{
    public Guid PortfolioId { get; init; }
    public string AssetName { get; init; }
}