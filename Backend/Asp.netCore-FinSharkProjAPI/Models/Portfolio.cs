using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.netCore_FinSharkProjAPI.Models
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public string AppUserId { get; set; }
        public int StockId { get; set; }

        public AppUser AppUser { get; set; } // Navigation property to AppUser

        public Stock Stock { get; set; } // Navigation property to Stock
    }
}
