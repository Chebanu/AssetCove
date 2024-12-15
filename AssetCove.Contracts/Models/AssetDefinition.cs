using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetCove.Contracts.Models;

[Table("AssetDefinitions")]
public class AssetDefinition
{
    [Key]
    [Column(nameof(Id))]
    public Guid Id { get; set; }

    [Required]
    [StringLength(5, MinimumLength = 3)]
    [Column(nameof(Ticker))]
    public string Ticker { get; set; }

    [Required]
    [Column(nameof(Name))]
    public string Name { get; set; }

    [Required]
    [Column(nameof(AssetType))]
    public AssetType AssetType { get; set; }
    public virtual ICollection<AssetTransaction> AssetTransactions { get; set; }
}

public enum AssetType
{
    Crypto,
    Stock,
    Precious
}