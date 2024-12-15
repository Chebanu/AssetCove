using AssetCove.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetCove.Domain.DbContexts;

public class AssetCoveDbContext : DbContext
{
    public DbSet<AssetTransaction> AssetTransactions { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<PortfolioShare> PortfolioShares { get; set; }
    public DbSet<UserAsset> UserAssets { get; set; }

    public AssetCoveDbContext(DbContextOptions<AssetCoveDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
}