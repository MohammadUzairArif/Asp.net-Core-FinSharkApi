using Asp.netCore_FinSharkProjAPI.Dtos.Stock;
using Asp.netCore_FinSharkProjAPI.Helpers;
using Asp.netCore_FinSharkProjAPI.Models;

namespace Asp.netCore_FinSharkProjAPI.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject query);
        Task<Stock?> GetStockByIdAsync(int id); // firstordefault can be null
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
