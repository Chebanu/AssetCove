using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetCove.Contracts.Models;

[Table("Portfolios")]
public class Portfolio
{
    [Key]
    [Column(nameof(Id))]
    public Guid Id { get; set; }

    [Required]
    [Column(nameof(UserId))]
    public Guid UserId { get; set; }

    [Required]
    [Column(nameof(Visibility))]
    public Visibility Visibility { get; set; }

    [Required]
    [Column(nameof(CreatedAt))]
    public DateTime CreatedAt { get; set; }

    [Column(nameof(LastUpdatedAt))]
    public DateTime LastUpdatedAt { get; set; }

    public List<UserAsset> UserAssets { get; set; }
    public List<PortfolioShare> ShareableEmails { get; set; }
}

public enum Visibility
{
    Public,
    Private,
    Shared
}