using System.ComponentModel.DataAnnotations;

namespace Asp.netCore_FinSharkProjAPI.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage = "Title must be 5 characters")]
        [MaxLength(200, ErrorMessage = "Title must be less than 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(200, ErrorMessage = "Content must be less than 200 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
