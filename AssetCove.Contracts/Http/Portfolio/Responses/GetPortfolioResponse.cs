namespace AssetCove.Contracts.Http.Portfolio.Responses;

public class GetPortfolioResponse
{
    public Guid PortfolioId { get; init; }
    public string PortfolioName { get; init; }
    public string PortfolioOwner { get; init; }
}