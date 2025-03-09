using AssetCove.Domain.Handlers;
using AssetCove.Domain.Repositories;

using Microsoft.Extensions.Logging;

using MediatR;

namespace AssetCove.Domain.Queries.Portfolio;

public class GetPortfolioByIdQuery : IRequest<GetPortfolioByIdResult>
{
    public string User { get; set; }
    public Guid PortfolioId { get; init; }
}


public class GetPortfolioByIdResult
{
    public Guid PortfolioId { get; init; }
    public string PortfolioName { get; init; }
    public string PortfolioOwner { get; init; }
    public string[] Errors { get; init; }
    public bool Success { get; init; }
}
public class GetPortfolioByIdQueryHandler : BaseRequestHandler<GetPortfolioByIdQuery, GetPortfolioByIdResult>
{
    private readonly IAssetCoveRepository _repository;
    public GetPortfolioByIdQueryHandler(ILogger<BaseRequestHandler<GetPortfolioByIdQuery,
                                        GetPortfolioByIdResult>> logger,
                                        IAssetCoveRepository repository) : base(logger)
    {
        _repository = repository;
    }

    protected override async Task<GetPortfolioByIdResult> HandleInternal(GetPortfolioByIdQuery request, CancellationToken cancellationToken)
    {
        var portfolio = await _repository.GetPortfolioByIdAsync(request.PortfolioId, request.User, cancellationToken);

        if (portfolio == null)
        {
            return new GetPortfolioByIdResult
            {
                Success = false,
                Errors = ["That portfolio doesn't exist"]
            };
        }

        return new GetPortfolioByIdResult
        {
            PortfolioId = portfolio.Id,
            PortfolioName = portfolio.Name,
            PortfolioOwner = portfolio.Username,
            Success = true
        };
    }
}
