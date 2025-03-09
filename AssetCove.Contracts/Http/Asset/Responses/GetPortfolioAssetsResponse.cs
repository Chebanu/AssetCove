namespace AssetCove.Contracts.Http.Asset.Responses;

public class GetPortfolioAssetsResponse
{
    public string PortfolioName { get; init; }
    public string AssetName { get; init; }
    public string PortfolioOwner { get; init; }
    public decimal Amount { get; init; }
}