using Asp.netCore_FinSharkProjAPI.Models;

namespace Asp.netCore_FinSharkProjAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);

        Task<Comment> CreateAsync(Comment commentModel);

        Task<Comment?> UpdateAsync(int id, Comment commentModel);

        Task<Comment?> DeleteAsync(int id);

    }
}
