using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCore_FinSharkProjAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
