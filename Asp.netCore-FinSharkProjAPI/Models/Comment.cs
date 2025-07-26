using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.netCore_FinSharkProjAPI.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        // foreign key
        public int? StockId { get; set; }

        // Navigation property
        public Stock? Stock { get; set; }

        //one to one relationship
         public string AppUserId { get; set; } 

        public AppUser AppUser { get; set; }
    }
}
