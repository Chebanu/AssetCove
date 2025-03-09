using AssetCove.Domain.Handlers;
using AssetCove.Domain.Repositories;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

using MediatR;

namespace AssetCove.Domain.Commands.Portfolios;

public class UpdatePortfolioNameCommand : IRequest<UpdatePortfolioNameResult>
{
    public Guid PortfolioId { get; init; }
    public string UpdatedName { get; init; }
    public string User { get; init; }
}

public class UpdatePortfolioNameResult
{
    public Guid PortfolioId { get; init; }
    public string[] Errors { get; init; }
    public bool Success { get; init; }
}

public class UpdatePortfolioNameCommandHandler : BaseRequestHandler<UpdatePortfolioNameCommand, UpdatePortfolioNameResult>
{
    private readonly IAssetCoveRepository _repository;
    private readonly UserManager<IdentityUser> _userManager;
    public UpdatePortfolioNameCommandHandler(ILogger<BaseRequestHandler<UpdatePortfolioNameCommand, UpdatePortfolioNameResult>> logger,
                                            IAssetCoveRepository repository,
                                            UserManager<IdentityUser> userManager) : base(logger)
    {
        _repository = repository;
        _userManager = userManager;
    }

    protected override async Task<UpdatePortfolioNameResult> HandleInternal(UpdatePortfolioNameCommand request, CancellationToken cancellationToken = default)
    {
        var portfolio = await _repository.GetPortfolioByIdAsync(request.PortfolioId, request.User, cancellationToken);

        if (portfolio == null)
        {
            return new UpdatePortfolioNameResult
            {
                Success = false,
                Errors = ["That portfolio doesn't exist"]
            };
        }

        if (portfolio.Username != request.User)
        {
            return new UpdatePortfolioNameResult
            {
                Success = false,
                Errors = ["You do not have permission to change portfolio's name"]
            };
        }

        portfolio.Name = request.UpdatedName;

        await _repository.UpdatePortfolioNameAsync(portfolio, cancellationToken);

        return
            new UpdatePortfolioNameResult
            {
                Success = true,
                PortfolioId = request.PortfolioId
            };
    }
}