using Asp.netCore_FinSharkProjAPI.Data;
using Asp.netCore_FinSharkProjAPI.Dtos.Stock;
using Asp.netCore_FinSharkProjAPI.Helpers;
using Asp.netCore_FinSharkProjAPI.Interfaces;
using Asp.netCore_FinSharkProjAPI.Mappers;
using Asp.netCore_FinSharkProjAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCore_FinSharkProjAPI.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext context;

        public StockRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Stock>> GetAllStocksAsync(QueryObject query)
        {
            var stocks = context.Stocks.Include(c=> c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))  //→ "SYMBOL", "symbol", "SyMbOl" sab valid hain. (case-insensitive check)

                {
                    stocks = query.IsDescending ? 
                             stocks.OrderByDescending(s => s.Symbol) : 
                             stocks.OrderBy(s => s.Symbol);
                }
            }
            // Add pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i=> i.Id == id);
        }


        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await context.Stocks.AddAsync(stockModel);
            await context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (existingStock == null)
            {
                return null; // Stock not found
            }
            // Update the existing stock with the new values from the DTO


            existingStock.UpdateFromDto(stockDto);

            await context.SaveChangesAsync();
            return existingStock;
        }


        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null; // Stock not found
            }
            context.Stocks.Remove(stockModel);
            await context.SaveChangesAsync();
            return stockModel; // Return the deleted stock model
        }

        public Task<bool> StockExists(int id)
        {
            return context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}
