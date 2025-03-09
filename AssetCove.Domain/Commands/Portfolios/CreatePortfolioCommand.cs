using AssetCove.Contracts.Models;
using AssetCove.Domain.Handlers;
using AssetCove.Domain.Repositories;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

using MediatR;

namespace AssetCove.Domain.Commands.Portfolios;

public class CreatePortfolioCommand : IRequest<CreatePortfolioResult>
{
    public string User { get; init; }
    public string PortfolioName { get; init; }
    public Visibility Visibility { get; init; }
    public List<string> ShareableList { get; init; }
}

public class CreatePortfolioResult
{
    public Guid PortfolioId { get; init; }
    public string[] Errors { get; init; }
    public bool Success { get; init; }
}

public class CreatePortfolioCommandHandler : BaseRequestHandler<CreatePortfolioCommand, CreatePortfolioResult>
{
    private readonly IAssetCoveRepository _repository;
    private readonly UserManager<IdentityUser> _userManager;
    public CreatePortfolioCommandHandler(ILogger<BaseRequestHandler<CreatePortfolioCommand,
                                        CreatePortfolioResult>> logger,
                                        IAssetCoveRepository repository,
                                        UserManager<IdentityUser> userManager) : base(logger)
    {
        _repository = repository;
        _userManager = userManager;
    }

    protected override async Task<CreatePortfolioResult> HandleInternal(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.IsPortfolioByNameExistAsync(request.User, request.PortfolioName, cancellationToken))
        {
            return new CreatePortfolioResult
            {
                PortfolioId = Guid.Empty,
                Success = false,
                Errors = ["Portfolio with that name is already exist, create portfolio with unique name"]
            };
        }

        if (request.ShareableList != null)
        {
            if (request.ShareableList.Contains(request.User))
            {
                return new CreatePortfolioResult
                {
                    PortfolioId = Guid.Empty,
                    Success = false,
                    Errors = ["Owner cannot be in the shared list"]

                };
            }

            var nonExistentUsernames = new List<string>();

            foreach (var username in request.ShareableList)
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    nonExistentUsernames.Add(username);
                }
            }

            if (nonExistentUsernames.Any())
            {
                return new CreatePortfolioResult
                {
                    PortfolioId = Guid.Empty,
                    Success = false,
                    Errors = [$"Non-existent users: {string.Join(", ", nonExistentUsernames)}"]
                };
            }

            var duplicates = request.ShareableList
                                .GroupBy(s => s)
                                .Where(g => g.Count() > 1)
                                .SelectMany(g => g)
                                .ToList();

            if (duplicates.Count > 0)
            {
                return new CreatePortfolioResult
                {
                    PortfolioId = Guid.Empty,
                    Success = false,
                    Errors = [$"Duplicate usernames: {string.Join(", ", duplicates.Distinct())}, {duplicates.Count} times"]
                };
            }
        }

        var porfolioId = Guid.NewGuid();

        var portfolio = new Portfolio
        {
            Id = porfolioId,
            Username = request.User,
            Name = request.PortfolioName,
            Visibility = request.Visibility,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = null,
            IsRemoved = false,
            ShareableEmails = request.Visibility switch
            {
                Visibility.Public => new List<PortfolioShare>(),
                Visibility.Private => new List<PortfolioShare>(),
                Visibility.Shared => request.ShareableList
                    ?.Select(email => new PortfolioShare
                    {
                        Id = Guid.NewGuid(),
                        PortfolioId = porfolioId,
                        Email = email
                    })
                    .ToList() ?? new List<PortfolioShare>(),
                _ => throw new ArgumentOutOfRangeException(nameof(request.Visibility), "Unknown visibility type")
            }
        };

        _ = await _repository.CreatePortfolioAsync(portfolio);

        return new CreatePortfolioResult
        {
            PortfolioId = portfolio.Id,
            Success = true
        };
    }
}

