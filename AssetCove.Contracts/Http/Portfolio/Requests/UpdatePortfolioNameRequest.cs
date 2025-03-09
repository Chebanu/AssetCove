namespace AssetCove.Contracts.Http.Portfolio.Requests;

public class UpdatePortfolioNameRequest
{
    public Guid PortfolioId { get; init; }
    public string Name { get; init; }
}