namespace AssetCove.Contracts.Http.Portfolio;

public class PortfolioUpdateNameRequest
{
    public Guid PortfolioId { get; init; }
    public string Name { get; init; }
}