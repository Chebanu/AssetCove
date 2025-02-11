using AssetCove.Domain.Handlers;
using AssetCove.Domain.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using MediatR;

namespace AssetCove.Domain.Queries;

public class GetUserPortfoliosQuery : IRequest<GetUserPortfoliosResult>
{
    public string User { get; init; }
    public string Owner { get; init; }
}

public class GetUserPortfoliosResult
{
    public List<UserPortfolioResult> UserPortfolioResults { get; init; }
    public string[] Errors { get; init; }
    public bool Success { get; init; }
}

public class UserPortfolioResult
{
    public Guid PortfolioId { get; init; }
    public string PortfolioName { get; init; }
}

public class GetPortfolioQueryHandler : BaseRequestHandler<GetUserPortfoliosQuery, GetUserPortfoliosResult>
{
    private readonly IAssetCoveRepository _repository;

    public GetPortfolioQueryHandler(ILogger<BaseRequestHandler<GetUserPortfoliosQuery,
                                    GetUserPortfoliosResult>> logger,
                                    IAssetCoveRepository repository) : base(logger)
    {
        _repository = repository;
    }

    protected override async Task<GetUserPortfoliosResult> HandleInternal(GetUserPortfoliosQuery request, CancellationToken cancellationToken)
    {
        var portfolios = await _repository.GetUserPortfoliosAsync(request.Owner, request.User, cancellationToken);

        if (portfolios == null || !portfolios.Any())
        {
            return new GetUserPortfoliosResult
            {
                Success = false,
                Errors = ["The owner doesn't have any portfolio available to you"]
            };
        }

        return new GetUserPortfoliosResult
        {
            Success = true,
            UserPortfolioResults = portfolios.Select(p => new UserPortfolioResult
            {
                PortfolioId = p.Id,
                PortfolioName = p.Name
            }).ToList()
        };
    }
}
