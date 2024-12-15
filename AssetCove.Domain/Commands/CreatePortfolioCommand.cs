using AssetCove.Contracts.Models;
using AssetCove.Domain.Handlers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssetCove.Domain.Commands;

public class CreatePortfolioCommand : IRequest<CreatePortfolioResult>
{
    public string PortfolioName { get; init; }
    public Visibility Visibility { get; init; }
    //public List<string> ShareableList { get; init; }
}

public class CreatePortfolioResult
{
    public Guid PortfolioId { get; init; }
    public string[] Errors { get; init; }
    public bool Success { get; init; }
}

public class CreatePortfolioCommandHandler : BaseRequestHandler<CreatePortfolioCommand, CreatePortfolioResult>
{
    public CreatePortfolioCommandHandler(ILogger<BaseRequestHandler<CreatePortfolioCommand, CreatePortfolioResult>> logger) : base(logger)
    {
    }

    protected override Task<CreatePortfolioResult> HandleInternal(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

