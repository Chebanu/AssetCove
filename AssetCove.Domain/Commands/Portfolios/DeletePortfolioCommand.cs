using AssetCove.Domain.Handlers;
using AssetCove.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace AssetCove.Domain.Commands.Portfolios;

public class DeletePortfolioCommand : IRequest<DeletePortfolioResult>
{
    public Guid PortfolioId { get; init; }
    public string User { get; init; }
}

public class DeletePortfolioResult
{
    public bool Success { get; init; }
    public string[] Errors { get; init; }
}

public class DeletePortfolioCommandHandler : BaseRequestHandler<DeletePortfolioCommand, DeletePortfolioResult>
{
    private readonly IAssetCoveRepository _repository;
    public DeletePortfolioCommandHandler(ILogger<BaseRequestHandler<DeletePortfolioCommand, DeletePortfolioResult>> logger,
                                            IAssetCoveRepository repository) : base(logger)
    {
        _repository = repository;
    }

    protected override async Task<DeletePortfolioResult> HandleInternal(DeletePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await _repository.GetPortfolioByIdAsync(request.PortfolioId, request.User, cancellationToken);

        if (portfolio == null)
        {
            return new DeletePortfolioResult
            {
                Success = false,
                Errors = ["That portfolio doesn't exist"]
            };
        }

        if (portfolio.Username != request.User)
        {
            return new DeletePortfolioResult
            {
                Success = false,
                Errors = ["You do not have permission to change portfolio's name"]
            };
        }

        portfolio.IsRemoved = true;
        portfolio.LastUpdatedAt = DateTime.UtcNow;

        await _repository.UpdatePortfolioAsync(portfolio, cancellationToken);

        return new DeletePortfolioResult { Success = true };
    }
}