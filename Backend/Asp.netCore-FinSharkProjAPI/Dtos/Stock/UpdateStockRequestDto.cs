using System.ComponentModel.DataAnnotations;

namespace Asp.netCore_FinSharkProjAPI.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Symbol must be at least 1 character")]
        [MaxLength(10, ErrorMessage = "Symbol must be less than 10 characters")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "Company name must be at least 1 character")]
        [MaxLength(100, ErrorMessage = "Company name must be less than 100 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 100000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.01, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Industry must be at least 1 character")]
        [MaxLength(50, ErrorMessage = "Industry must be less than 50 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 500000000)]
        public long MarketCap { get; set; }
    }


}
