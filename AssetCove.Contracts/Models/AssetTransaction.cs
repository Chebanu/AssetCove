using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetCove.Contracts.Models;

[Table("AssetTransactions")]
public class AssetTransaction
{
    [Key]
    [Column(nameof(Id))]
    public Guid Id { get; set; }

    [ForeignKey(nameof(UserAssetId))]
    public UserAsset UserAsset { get; set; }

    [Required]
    [Column(nameof(UserAssetId))]
    public Guid UserAssetId { get; set; }

    [Required]
    [Column(nameof(Amount))]
    public decimal Amount { get; set; }

    [Required]
    [Column(nameof(PricePerUnit))]
    public decimal PricePerUnit { get; set; }

    [Required]
    [Column(nameof(TransactionType))]
    public TransactionType TransactionType  { get; set; }

    [Required]
    [Column(nameof(Timestamp))]
    public DateTime Timestamp { get; set; }
}

public enum TransactionType
{
    Buy,
    Sell
}