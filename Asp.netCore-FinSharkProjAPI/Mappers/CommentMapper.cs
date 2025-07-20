using Asp.netCore_FinSharkProjAPI.Dtos.Comment;
using Asp.netCore_FinSharkProjAPI.Models;

namespace Asp.netCore_FinSharkProjAPI.Mappers
{
    public static class CommentMapper
    {
        /* Model → DTO
       public static ModelNameDto ToDto(this ModelName model)
       {
           return new ModelNameDto
           {
               Id = model.Id,
               Property1 = model.Property1,
               Property2 = model.Property2
               // Map remaining properties
           };
       }
       */
        public static CommentDto ToCommentDto(this Comment commentModel) { 
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }
    }
}
