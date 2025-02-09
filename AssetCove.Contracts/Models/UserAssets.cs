using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetCove.Contracts.Models;

[Table("UserAssets")]
public class UserAsset
{
    [Key]
    [Column(nameof(Id))]
    public Guid Id { get; set; }

    [Required]
    [Column(nameof(Amount))]
    public decimal Amount { get; set; }

    [Required]
    [Column(nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; }

    [Column(nameof(LastUpdatedAt))]
    public DateTime LastUpdatedAt { get; set; }

    [Required]
    [Column(nameof(IsRemoved))]
    public bool IsRemoved { get; set; }

    [ForeignKey(nameof(PortfolioId))]
    public virtual Portfolio Portfolio { get; set; }

    [Required]
    [Column(nameof(PortfolioId))]
    public Guid PortfolioId { get; set; }

    public virtual ICollection<AssetTransaction> Transactions { get; set; }
}