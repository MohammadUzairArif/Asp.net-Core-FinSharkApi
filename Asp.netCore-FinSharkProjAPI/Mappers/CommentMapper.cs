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
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        /* DTO → Model
        public static ModelName ToModel(this ModelNameDto dto)
        {
            return new ModelName
            {
                Id = dto.Id,
                Property1 = dto.Property1,
                Property2 = dto.Property2
                // Map remaining properties
            };
        }
        */
        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }

        
        public static Comment ToCommentFromUpdateDto(this UpdateCommentRequestDto updateDto)
        {
            return new Comment
            {
                Title = updateDto.Title,
                Content = updateDto.Content
            };
        }
    }
}
