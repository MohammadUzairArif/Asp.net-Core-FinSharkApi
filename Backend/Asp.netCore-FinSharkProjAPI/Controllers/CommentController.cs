using Asp.netCore_FinSharkProjAPI.Dtos.Comment;
using Asp.netCore_FinSharkProjAPI.Extentions;
using Asp.netCore_FinSharkProjAPI.Helpers;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Mappers;
using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IFMPService fMPService;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager, IFMPService fMPService)
        {
            this.commentRepo = commentRepo;
            this.stockRepo = stockRepo;
            this.userManager = userManager;
            this.fMPService = fMPService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery]CommentQueryObject queryObject)
        {
            // data validation
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await commentRepo.GetAllCommentsAsync(queryObject);
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

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> CreateComment([FromRoute] string symbol,[FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var stock = await stockRepo.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                stock = await fMPService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock doesnot exists");
                }
                else
                {
                    await stockRepo.CreateAsync(stock);   
                }
            }
                    var userName = User.GetUsername();
            var appUser = await userManager.FindByNameAsync(userName);


            var commentModel = commentDto.ToCommentFromCreate(stock.Id); // Convert DTO to Model
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
