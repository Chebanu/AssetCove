using AssetCove.Contracts.Models;

namespace AssetCove.Contracts.Http.Portfolio.Requests;

public class CreatePortfolioRequest
{
    public string PortfolioName { get; init; }
    public Visibility Visibility { get; init; }
    public List<string> ShareableList { get; init; }
}