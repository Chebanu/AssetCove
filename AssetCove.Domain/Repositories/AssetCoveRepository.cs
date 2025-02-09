﻿using AssetCove.Contracts.Models;
using AssetCove.Domain.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AssetCove.Domain.Repositories;

public interface IAssetCoveRepository
{
    //Portfolio
    Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken = default);
    Task<Portfolio> GetPortfolioByIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task<bool> IsPortfolioByNameExistAsync(string username, string portfolioName, CancellationToken cancellationToken = default);
    Task<Portfolio> UpdatePortfolioNameAsync(Portfolio portfolio, CancellationToken cancellationToken = default);

    //Transaction
    Task<AssetTransaction> CreateTransactionAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default);
    Task<AssetTransaction> GetTransactionAsync(Guid assetTransactionId, CancellationToken cancellationToken = default);
    Task<AssetTransaction> UpdateTransactionAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default);

    //UserAsset
    Task<UserAsset> CreateUserAssetAsync(UserAsset userAsset, CancellationToken cancellationToken = default);
    Task<UserAsset> GetUserAssetAsync(Guid assetId, CancellationToken cancellationToken = default);
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

    public async Task<Portfolio> GetPortfolioByIdAsync(Guid portfolioId, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios.SingleOrDefaultAsync(x => x.Id == portfolioId, cancellationToken);
    }

    public async Task<bool> IsPortfolioByNameExistAsync(string username, string portfolioName, CancellationToken cancellationToken = default)
    {
        return await _context.Portfolios
            .AnyAsync(x => x.Username == username && x.Name == portfolioName, cancellationToken);
    }

    public async Task<Portfolio> UpdatePortfolioNameAsync(Portfolio portfolio, CancellationToken cancellationToken = default)
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
    public async Task<UserAsset> GetUserAssetAsync(Guid assetId, CancellationToken cancellationToken = default)
    {
        return await _context.UserAssets.SingleOrDefaultAsync(x => x.Id == assetId, cancellationToken);
    }

    public async Task<UserAsset> UpdateUserAssetAsync(UserAsset userAsset, CancellationToken cancellationToken = default)
    {
        _ = _context.UserAssets.Update(userAsset);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return userAsset;
    }

    #endregion
}