using AssetCove.Contracts.Models;
using AssetCove.Domain.DbContexts;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace AssetCove.Domain.Repositories;

public interface IAssetCoveRepository
{
    //Portfolio
    Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken = default);
    Task<Portfolio> GetPortfolioByIdAsync(Guid portfolioId, string user, CancellationToken cancellationToken = default);
    Task<bool> IsPortfolioByNameExistAsync(string username, string portfolioName, CancellationToken cancellationToken = default);
    Task<List<Portfolio>> GetUserPortfoliosAsync(string owner, string user, CancellationToken cancellationToken = default);
    Task<Portfolio> UpdatePortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken = default);

    //Transaction
    Task<AssetTransaction> CreateTransactionAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default);
    Task<AssetTransaction> GetTransactionAsync(Guid assetTransactionId, CancellationToken cancellationToken = default);
    Task<AssetTransaction> UpdateTransactionAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default);

    //UserAsset
    Task<UserAsset> CreateUserAssetAsync(UserAsset userAsset, CancellationToken cancellationToken = default);
    Task<UserAsset> GetUserAssetAsync(Guid assetId, string user, CancellationToken cancellationToken = default);
    Task<UserAsset> UpdateUserAssetAsync(UserAsset userAsset, CancellationToken cancellationToken = default);

    //Asset Definition
}

public partial class AssetCoveRepository : IAssetCoveRepository
{
    private readonly AssetCoveDbContext _context;

    public AssetCoveRepository(AssetCoveDbContext context)
    {
        _context = context;
    }

    #region Portfolio
    public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken = default)
    {
        _ = await _context.Portfolios.AddAsync(portfolio, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return portfolio;
    }

    public async Task<Portfolio> GetPortfolioByIdAsync(Guid portfolioId, string user, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios
            .Where(p =>
                p.Id == portfolioId &&
                (
                    p.Visibility == Visibility.Public ||
                    (p.Visibility == Visibility.Shared && p.ShareableEmails.Any(e => e.Email == user)) ||
                    p.Username == user
                ) && p.IsRemoved == false
            )
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsPortfolioByNameExistAsync(string username, string portfolioName, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios
            .AnyAsync(x => x.Username == username && x.Name == portfolioName && x.IsRemoved == false, cancellationToken);
    }

    public async Task<List<Portfolio>> GetUserPortfoliosAsync(string owner, string user, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios
            .Where(p =>
                p.Username == owner &&
                (
                    p.Visibility == Visibility.Public ||
                    (p.Visibility == Visibility.Shared && p.ShareableEmails.Any(e => e.Email == user)) ||
                    p.Username == user
                ) && p.IsRemoved == false
            )
            .ToListAsync(cancellationToken);
    }

    public async Task<Portfolio> UpdatePortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken = default)
    {
        _ = _context.Portfolios.Update(portfolio);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return portfolio;
    }

    #endregion

    #region Transaction
    public async Task<AssetTransaction> CreateTransactionAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default)
    {
        _ = await _context.AssetTransactions.AddAsync(assetTransaction, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return assetTransaction;
    }

    public async Task<AssetTransaction> GetTransactionAsync(Guid assetTransactionId, CancellationToken cancellationToken = default)
    {
        return await _context.AssetTransactions.SingleOrDefaultAsync(x => x.Id == assetTransactionId, cancellationToken);
    }
    public async Task<AssetTransaction> UpdateTransactionAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default)
    {
        _ = _context.AssetTransactions.Update(assetTransaction);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return assetTransaction;
    }

    #endregion

    #region UserAsset
    public async Task<UserAsset> CreateUserAssetAsync(UserAsset userAsset, CancellationToken cancellationToken = default)
    {
        _ = await _context.UserAssets.AddAsync(userAsset, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return userAsset;
    }
    public async Task<UserAsset> GetUserAssetAsync(Guid assetId, string user, CancellationToken cancellationToken = default)
    {
        return await _context.UserAssets
            .Where(a =>
                a.Id == assetId &&
                a.IsRemoved == false &&
                (
                    a.Portfolio.Visibility == Visibility.Public ||
                    (a.Portfolio.Visibility == Visibility.Shared && a.Portfolio.ShareableEmails.Any(e => e.Email == user)) ||
                    a.Portfolio.Username == user
                )
            )
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<UserAsset> UpdateUserAssetAsync(UserAsset userAsset, CancellationToken cancellationToken = default)
    {
        _ = _context.UserAssets.Update(userAsset);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return userAsset;
    }

    #endregion
}