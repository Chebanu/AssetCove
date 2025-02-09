using AssetCove.Contracts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetCove.Domain.DbContexts;

public class AssetCoveDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<UserAsset> UserAssets { get; set; }
    public DbSet<AssetTransaction> AssetTransactions { get; set; }
    public DbSet<PortfolioShare> PortfolioShares { get; set; }
    public DbSet<AssetDefinition> AssetDefinition { get; set; }

    public AssetCoveDbContext(DbContextOptions<AssetCoveDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
}