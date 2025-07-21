using Asp.netCore_FinSharkProjAPI.Dtos.Comment;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netCore_FinSharkProjAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepo;
        private readonly IStockRepository stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            this.commentRepo = commentRepo;
            this.stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await commentRepo.GetAllCommentsAsync();
            var commentDtos = comments.Select(c => c.ToCommentDto()); // Convert to DTOs
            if (comments == null)
            {
                return NotFound("No comments found.");
            }
            return Ok(commentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await commentRepo.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }
            return Ok(comment.ToCommentDto()); // Convert to DTO
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId,[FromBody] CreateCommentDto commentDto)
        {
            if (!await stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock doesnot exists");
            }
            var commentModel = commentDto.ToCommentFromCreate(stockId); // Convert DTO to Model
            // Here you would typically save the comment to the database
             await commentRepo.CreateAsync(commentModel); // Save the comment using the repository
            // For now, we will just return the created comment
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }
    }
}
