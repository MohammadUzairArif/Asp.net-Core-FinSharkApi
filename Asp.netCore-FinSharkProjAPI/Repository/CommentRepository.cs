using Asp.netCore_FinSharkProjAPI.Data;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCore_FinSharkProjAPI.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<List<Comment>> GetAllCommentsAsync()
        {
           return await context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await context.Comments.FindAsync(id);
        }
        public async Task<Comment> CreateAsync(Comment commentModel)
        {
           await context.Comments.AddAsync(commentModel);
          await context.SaveChangesAsync();
              return commentModel;
        }
    }
}
