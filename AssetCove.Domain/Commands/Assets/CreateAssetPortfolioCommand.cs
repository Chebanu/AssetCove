using AssetCove.Domain.Handlers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssetCove.Domain.Commands.Assets;

public class CreateAssetPortfolioCommand : IRequest<CreateAssetPortolioResult>
{
    public Guid PortfolioId { get; init; }
    public string User { get; init; }
    public string AssetName { get; init; }
}

public class CreateAssetPortolioResult
{
    public string PortfolioName { get; init; }
    public string AssetName { get; init; }
    public string[] Errors { get; init; }
    public bool Success { get; init; }
}

public class CreateAssetPortfolioHandler : BaseRequestHandler<CreateAssetPortfolioCommand, CreateAssetPortolioResult>
{
    public CreateAssetPortfolioHandler(ILogger<BaseRequestHandler<CreateAssetPortfolioCommand, CreateAssetPortolioResult>> logger) : base(logger)
    {

    }

    protected override Task<CreateAssetPortolioResult> HandleInternal(CreateAssetPortfolioCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}