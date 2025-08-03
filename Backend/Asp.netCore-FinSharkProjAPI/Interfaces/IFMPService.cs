using Asp.netCore_FinSharkProjAPI.Models;

namespace Asp.netCore_FinSharkProjAPI.Interfaces
{
    public interface IFMPService
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
    }
}
