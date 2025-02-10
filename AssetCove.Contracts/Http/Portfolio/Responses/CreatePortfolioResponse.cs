namespace AssetCove.Contracts.Http.Portfolio.Responses;

public class CreatePortfolioResponse
{
    public Guid PortfolioId { get; init; }
    public bool Success { get; init; }
    public string[] Errors { get; init; }
}
