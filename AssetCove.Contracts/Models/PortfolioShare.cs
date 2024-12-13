using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetCove.Contracts.Models;

[Table("PortfolioShares")]
public class PortfolioShare
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column(nameof(PortfolioId))]
    public Guid PortfolioId { get; set; }

    [ForeignKey(nameof(PortfolioId))]
    public Portfolio Portfolio { get; set; }

    [Required]
    [Column(nameof(Email))]
    public string Email { get; set; }
}