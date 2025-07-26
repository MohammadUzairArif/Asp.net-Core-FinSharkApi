using Asp.netCore_FinSharkProjAPI.Models;

namespace Asp.netCore_FinSharkProjAPI.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
    }
}
