using Asp.netCore_FinSharkProjAPI.Data;
using Asp.netCore_FinSharkProjAPI.Dtos.Stock;
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
        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await context.Stocks.FindAsync(id);
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


    }
}
