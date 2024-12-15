using AssetCove.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Polly.Fallback;

namespace AssetCove.Domain.DbContexts;

public class AssetCoveDbContext : DbContext
{
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<UserAsset> UserAssets { get; set; }
    public DbSet<AssetTransaction> AssetTransactions { get; set; }
    public DbSet<PortfolioShare> PortfolioShares { get; set; }
    public DbSet<AssetDefinition> AssetDefinition { get; set; }

    public AssetCoveDbContext(DbContextOptions<AssetCoveDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetDefinition>()
            .HasIndex(ad => new { ad.Ticker, ad.Name, ad.AssetType })
            .IsUnique();
    }
}