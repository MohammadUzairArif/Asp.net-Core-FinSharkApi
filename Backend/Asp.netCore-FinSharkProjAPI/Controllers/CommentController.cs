using Asp.netCore_FinSharkProjAPI.Dtos.Comment;
using Asp.netCore_FinSharkProjAPI.Extentions;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Mappers;
using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Asp.netCore_FinSharkProjAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepo;
        private readonly IStockRepository stockRepo;
        private readonly UserManager<AppUser> userManager;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager)
        {
            this.commentRepo = commentRepo;
            this.stockRepo = stockRepo;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // data validation
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await commentRepo.GetAllCommentsAsync();
            var commentDtos = comments.Select(c => c.ToCommentDto()); // Convert to DTOs
            if (comments == null)
            {
                return NotFound("No comments found.");
            }
            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await commentRepo.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }
            return Ok(comment.ToCommentDto()); // Convert to DTO
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId,[FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock doesnot exists");
            }

            var userName = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(userName);


            var commentModel = commentDto.ToCommentFromCreate(stockId); // Convert DTO to Model
            commentModel.AppUserId = appUser.Id; // Set the AppUserId from the authenticated user

               // Here you would typically save the comment to the database
            await commentRepo.CreateAsync(commentModel); // Save the comment using the repository
            // For now, we will just return the created comment
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await commentRepo.UpdateAsync(id,updateDto.ToCommentFromUpdateDto());

            if (comment == null)
            {
                return NotFound("Comment not found");
            }
             return Ok(comment.ToCommentDto()); // Convert to DTO
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found");
            }
            return NoContent(); // Return 204 No Content on successful deletion
        }
    }
}
