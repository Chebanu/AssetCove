using AssetCove.Contracts.Models;

namespace AssetCove.Domain.Repositories;

public interface IAssetCoveRepository
{
    //Portfolio
    Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio, CancellationToken cancellationToken = default);
    Task<Portfolio> GetPortfolioByIdAsync(Guid portfolioId, CancellationToken cancellationToken = default);
    Task<Portfolio> UpdatePortfolioVisibilityAsync(Portfolio portfolio, CancellationToken cancellationToken = default);
    Task<Portfolio> UpdatePortfolioNameAsync(Portfolio portfolio, CancellationToken cancellationToken = default);
    Task<Guid> DeletePortfolioAsync(Guid portfolioId, CancellationToken cancellationToken = default);

    //Transaction
    Task<AssetTransaction> CreateTransactionAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default);
    Task<AssetTransaction> GetTransactionAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default);
    Task<AssetTransaction> UpdateTransactionAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default);
    Task<Guid> DeleteTransactionAsync(Guid transactionId, CancellationToken cancellationToken = default);

    //UserAsset
    Task<UserAsset> CreateUserAssetAsync(UserAsset userAsset, CancellationToken cancellationToken = default);
    Task<UserAsset> GetUserAssetAsync(Guid assetId, CancellationToken cancellationToken = default);
    Task<UserAsset> DeleteAssetById(Guid assetId, CancellationToken cancellationToken = default);
    
    //

}

public partial class AssetCoveRepository
{
}
