using Asp.netCore_FinSharkProjAPI.Data;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCore_FinSharkProjAPI.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext context;

        public PortfolioRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await context.Portfolios.AddAsync(portfolio);
            await context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await context.Portfolios
                .Where(p => p.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.Stock.Id,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }
    }
}
