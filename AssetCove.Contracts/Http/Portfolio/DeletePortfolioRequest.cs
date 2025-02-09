namespace AssetCove.Contracts.Http.Portfolio;

public class DeletePortfolioRequest
{
    public Guid PortfolioId { get; init; }
    public bool IsRemoved { get; init; }
}